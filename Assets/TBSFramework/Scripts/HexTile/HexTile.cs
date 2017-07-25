using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
            this.atlas = atlas;

            UIGraphics = new TileGraphics(atlas, Position);
            TerrainGraphics = new TileGraphics(atlas, Position);

            owner = Group.Null;
            improvement = ImprovementType.Null;
            TileTimer = new TurnTimer(TurnManager.Instance);

            Stats = new TileStats(owner);
            OwnerModifiers = new TileGroupModifiers(owner);


            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
            GameManager.Instance.Play.StateEnabled += OnGameStart;
            TileTimer.TimerExpired += OnTimerExpire;
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
        protected ImprovementType improvement;
        public ImprovementType Improvement
        {
            get { return improvement; }
            set
            {
                improvement.OnDeactivation(this);

                if (value == null)
                    improvement = ImprovementType.Null;
                else
                    improvement = value;

                improvement.OnActivation(this);
            }
        }

        public TurnTimer TileTimer;
        public event Action<HexTile> TileTimerExpired;
        public void OnTimerExpire()
        {
            if (TileTimerExpired != null)
                TileTimerExpired(this);
        }

        [SerializeField]
        protected Activity currentActivity;
        public Activity CurrentActivity
        {
            get { return currentActivity; }
        }


        [SerializeField]
        protected Group owner;
        public Group Owner
        {
            get { return owner; }
            set
            {
                Group oldOwner = owner;

                if (value == null)
                    owner = Group.Null;
                else
                    owner = value;

                Stats.Owner = owner;
                OwnerModifiers.Owner = owner;
            }
        }

        void OnTurnEnd(BaseTurn turn)
        {
            if (Land != null)
                Land.OnTurnEnd(turn, this);

            if (Improvement != null)
                Improvement.OnTurnEnd(turn, this);
        }

        void OnTurnSetUp(BaseTurn turn)
        {
            if (Land != null)
                Land.OnTurnSetUp(turn, this);

            if (Improvement != null)
                Improvement.OnTurnSetUp(turn, this);
        }

        void OnTurnStart(BaseTurn turn)
        {
            if (Land != null)
                Land.OnTurnStart(turn, this);

            if (Improvement != null)
                Improvement.OnTurnStart(turn, this);
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

        public TileStats Stats;
        public TileGroupModifiers OwnerModifiers;


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
                    GameManager.Instance.Play.StateEnabled -= OnGameStart;
                    TileTimer.TimerExpired += OnTimerExpire;
                    UIGraphics = null;
                    TerrainGraphics = null;
                    Land.OnDeactivation(this);
                    Improvement.OnDeactivation(this);
                    Land = null;
                    Improvement = null;

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