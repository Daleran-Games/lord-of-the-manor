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

        public HexTile(HexCoordinates coor, Vector2Int gcoord, Vector3 pos, int id, TileAtlas atlas)
        {
            HexPosition = coor;
            Position = pos;
            GridPosition = gcoord;
            ID = id;
            MaxID = id;
            
            //stats = new Dictionary<StatType, int>();
            this.atlas = atlas;

            UIGraphics = new TileGraphics(atlas, Position);
            TerrainGraphics = new TileGraphics(atlas, Position);

            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnBegan += OnTurnBegin;
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
        protected HexCoordinates hexPosition;
        public HexCoordinates HexPosition { get { return hexPosition; } protected set { hexPosition = value; } }

        [SerializeField]
        [ReadOnly]
        protected Vector2Int gridPos;
        public Vector2Int GridPosition { get { return gridPos; } protected set { gridPos = value; } }

        [SerializeField]
        [ReadOnly]
        protected Vector3 position = Vector3.zero;
        public Vector3 Position { get { return position; } protected set { position = value; } }

        [SerializeField]
        protected Group owner;
        public Group Owner { get { return owner; } }

        [SerializeField]
        protected Unit occupier;
        public Unit Occupuer { get { return occupier; } }


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

        void OnTurnEnd(BaseTurn turn)
        {
            if (Land != null)
                Land.OnTurnEnd(turn, this);

            if (Improvement != null)
                Improvement.OnTurnEnd(turn, this);
        }

        void OnTurnBegin(BaseTurn turn)
        {
            if (Land != null)
                Land.OnTurnBegin(turn, this);

            if (Improvement != null)
                Improvement.OnTurnBegin(turn, this);
        }

        void OnGameStart(GameState state)
        {
            if (Land != null)
                Land.OnGameStart(this);

            if (Improvement != null)
                Improvement.OnGameStart(this);
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

        public Stat MovementCost
        {
            get
            {
                if (land != null && improvement != null)
                    return land.MovementCost + improvement.MovementCost;
                else if (land != null && improvement == null)
                    return land.MovementCost;
                else
                    return new Stat(Stat.Category.MovementCost, 1);
            }
        }

        public Stat DefenseBonus
        {
            get
            {
                if (land != null && improvement != null)
                    return land.DefenseBonus + improvement.DefenseBonus;
                else if (land != null && improvement == null)
                    return land.DefenseBonus;
                else
                    return new Stat(Stat.Category.DefenseBonus, 0);
            }
        }

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
                    TurnManager.Instance.TurnBegan -= OnTurnBegin;
                    GameManager.Instance.StateChanged -= OnGameStart;
                    UIGraphics = null;
                    TerrainGraphics = null;
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