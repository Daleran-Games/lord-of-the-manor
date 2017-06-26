using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Game;

namespace DaleranGames.TBSFramework
{

    [System.Serializable]
    public class HexTile : IDisposable
    {
        public HexTile(HexCoordinates coor, Vector3 pos, int id)
        {
            Coord = coor;
            Position = pos;
            ID = id;
            MaxID = id;

            graphics = new Dictionary<HexLayers, Vector2Int>();

            TurnManager.Instance.TurnChanged += OnTurnChange;
            GameManager.Instance.StateChanged += OnGameStart;
        }

        #region Tile Properties

        [SerializeField]
        [ReadOnly]
        protected int id = 0;
        public int ID { get { return id; } protected set { id = value; } }
        public static int MaxID = 0;

        [SerializeField]
        [ReadOnly]
        protected HexCoordinates coord;
        public HexCoordinates Coord { get { return coord; } protected set { coord = value; } }

        [SerializeField]
        [ReadOnly]
        protected Vector3 position = Vector3.zero;
        public Vector3 Position { get { return position; } protected set { position = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte elevation = 0;
        public byte Elevation { get { return elevation; } set { elevation = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte moisture = 0;
        public byte Moisture { get { return moisture; } set { moisture = value; } }

        [SerializeField]
        [ReadOnly]
        protected Clan owner;
        public Clan Owner { get { return owner; } set { owner = value; } }

        #endregion

        #region Tile Components

        [SerializeField]
        protected LandType land;
        public virtual LandType Land
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
        protected ImprovementType improvement;
        public virtual ImprovementType Improvement
        {
            get { return improvement; }
            set
            {
                if (improvement != null)
                    improvement.OnDeactivation(this);

                improvement = value;

                if (improvement != null)
                    improvement.OnActivation(this);
            }
        }

        protected TileComponent component;
        public virtual TileComponent HexComponent
        {
            get { return component; }
            set
            {
                component = value;
            }
        }


        void OnTurnChange(BaseTurn turn)
        {
            if (Land != null)
                Land.OnTurnChange(turn, this);

            if (Improvement != null)
                Improvement.OnTurnChange(turn, this);
        }

        void OnGameStart(GameState state)
        {
            if (Land != null)
                Land.OnGameStart(this);

            if (Improvement != null)
                Improvement.OnGameStart(this);
        }

        #endregion

        #region Graphics

        public Action<HexTile, HexLayers> TileGraphicsChange;
        protected Dictionary<HexLayers, Vector2Int> graphics;

        public Vector2Int GetGraphicsAtLayer(HexLayers layer)
        {
            Vector2Int graphic = Vector2Int.zero;
            if (graphics.TryGetValue(layer, out graphic))
            {
                //Debug.Log("Cell#" + ID + " graphic: " + graphic + " layer " + layer.ToString());
                return graphic;
            } else
                return graphic;
        }

        public bool ContainsGraphic (HexLayers layer)
        {
            return graphics.ContainsKey(layer);
        }

        public void AddGraphic (HexLayers layer, Vector2Int coord)
        {
            if (!graphics.ContainsKey(layer))
            {
                graphics.Add(layer, coord);

                if (TileGraphicsChange != null)
                    TileGraphicsChange(this, layer);
            }
        }

        public void RemoveGraphic (HexLayers layer)
        {
            if (graphics.ContainsKey(layer))
            {
                graphics.Remove(layer);

                if (TileGraphicsChange != null)
                    TileGraphicsChange(this, layer);
            }
        }

        public void ClearGraphics ()
        {
            graphics.Clear();
        }

        #endregion


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TurnManager.Instance.TurnChanged -= OnTurnChange;
                    GameManager.Instance.StateChanged -= OnGameStart;
                    TileGraphicsChange = null;
                    Land.OnDeactivation(this);
                    Improvement.OnDeactivation(this);
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