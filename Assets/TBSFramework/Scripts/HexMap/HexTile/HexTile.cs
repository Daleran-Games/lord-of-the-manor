using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{

    [System.Serializable]
    public class HexTile : IDisposable, ITooltipable
    {
        public HexTile(HexCoordinates coor, Vector2Int gcoord, Vector3 pos, int id, TileAtlas atlas)
        {
            Coordinate = coor;
            Position = pos;
            GridPosition = gcoord;
            ID = id;
            MaxID = id;
            
            this.atlas = atlas;

            UIGraphics = new TileGraphics(atlas, Position);
            TerrainGraphics = new TileGraphics(atlas, Position);

            owner = Group.Null;
            feature = FeatureType.Null;
            Counters = new TurnCounters(TurnManager.Instance);

            Stats = new TileStats(owner);
            OwnerModifiers = new TileGroupModifiers(owner);

            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
        }

        #region Tile Properties
        
        [SerializeField]
        [ReadOnly]
        protected int id = 0;
        public int ID { get { return id; } protected set { id = value; } }
        public static int MaxID = 0;

        [SerializeField]
        [ReadOnly]
        protected HexCoordinates coordinate;
        public HexCoordinates Coordinate { get { return coordinate; } protected set { coordinate = value; } }

        [SerializeField]
        [ReadOnly]
        protected Vector2Int gridPos;
        public Vector2Int GridPosition { get { return gridPos; } protected set { gridPos = value; } }

        [SerializeField]
        [ReadOnly]
        protected Vector3 position = Vector3.zero;
        public Vector3 Position { get { return position; } protected set { position = value; } }


        public string Info
        {
            get
            {
                return GenerateTooltip();
            }
        }

        public string GenerateTooltip()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(Land.Name.ToHeaderStyle());

            if (Feature != FeatureType.Null && Feature != null)
                sb.AppendLine(" - <b>" + Feature.Name + "</b>");
            else
                sb.AppendLine("");


            sb.AppendLine("<b>Tile Stats</b>");
            sb.AppendLine(Stats.Info);

            if (OwnerModifiers.Count > 0)
            {
                sb.AppendLine("<b>Owner Modifiers</b>");
                sb.AppendLine(OwnerModifiers.Info);
            }

            if (Counters.Count > 0)
            {
                sb.AppendLine("<b>Timers</b>");
                sb.AppendLine(Counters.Info);
            }


            return sb.ToString();
        }

        #endregion

        #region Tile Components

        [SerializeField]
        protected LandType land;
        public LandType Land
        {
            get { return land; }
            set
            {
                if (land != null)
                    land.OnDeactivation(this);

                land = value;

                if (land != null)
                    land.OnActivation(this);
            }
        }

        [SerializeField]
        string debugFeatureName;

        protected FeatureType feature;
        public FeatureType Feature
        {
            get { return feature; }
            set
            {
                feature.OnDeactivation(this);
                SwitchFeatureWithNoDeactiviation(value);
            }
        }

        public void SwitchFeatureWithNoDeactiviation (FeatureType newFeature)
        {
            if (newFeature == null)
            {
                feature = FeatureType.Null;
                debugFeatureName = "Null";
            }
            else
            {
                feature = newFeature;
                debugFeatureName = feature.Name;
            }
            feature.OnActivation(this);
        }

        public void SwitchFeatureWithNoActiviation(FeatureType newFeature)
        {
            feature.OnDeactivation(this);
            if (newFeature == null)
            {
                feature = FeatureType.Null;
                debugFeatureName = "Null";
            }
            else
            {
                feature = newFeature;
                debugFeatureName = feature.Name;
            }
        }

        [SerializeField]
        protected bool paused = false;
        public bool Paused { get { return paused; } set { paused = value; } }

        [SerializeField]
        protected bool pausedOverride = false;
        public bool PausedOverride { get { return pausedOverride; } set { pausedOverride = value; } }

        void OnTurnEnd(BaseTurn turn)
        {
            if (Feature != null && !Paused)
                Feature.OnTurnEnd(turn, this);
        }

        void OnTurnSetUp(BaseTurn turn)
        {
            if (Feature != null && !Paused)
                Feature.OnTurnSetUp(turn, this);
        }

        void OnTurnStart(BaseTurn turn)
        {
            if (Feature != null && !Paused)
                Feature.OnTurnStart(turn, this);
        }

        [SerializeField]
        protected Group owner;
        public Group Owner
        {
            get { return owner; }
            set
            {
                Group oldOwner = owner;
                oldOwner.OwnedTiles.Remove(this);
                if (value == null || value == Group.Null)
                {
                    owner = Group.Null;
                    UIGraphics.Remove(TileLayers.Border);
                }  
                else
                {
                    owner = value;
                    UIGraphics.Add(TileLayers.Border, GameDatabase.Instance.TileGraphics["Border_2px_All"]);
                    owner.OwnedTiles.Add(this);
                }
                
                Stats.Owner = owner;
                OwnerModifiers.Owner = owner;
            }
        }

        #endregion

        #region TileStats

        [SerializeField]
        [ReadOnly]
        protected byte elevation = 0;
        public byte Elevation { get { return elevation; } set { elevation = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte moisture = 0;
        public byte Moisture { get { return moisture; } set { moisture = value; } }

        public TileStats Stats;
        public TileGroupModifiers OwnerModifiers;
        public TurnCounters Counters;

        #endregion

        #region Graphics

        TileAtlas atlas;
        public TileGraphics UIGraphics;
        public TileGraphics TerrainGraphics;


        #endregion


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TurnManager.Instance.TurnEnded -= OnTurnEnd;
                    TurnManager.Instance.TurnSetUp -= OnTurnSetUp;
                    TurnManager.Instance.TurnStart += OnTurnStart;
                    UIGraphics = null;
                    TerrainGraphics = null;
                    Land.OnDeactivation(this);
                    Feature.OnDeactivation(this);
                    Land = null;
                    Feature = null;

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion



    }

}