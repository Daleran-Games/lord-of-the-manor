using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;

namespace DaleranGames.Tools
{
    public class HexStrategyCamera : PixelPerfectMoveZoomCamera
    {
        HexGrid grid;

        [SerializeField]
        HexTile currentTile;
        public HexTile CurrentTile
        {
            get { return currentTile; }
            protected set
            {
                currentTile = value;

            }
        }

        public event System.Action<HexTile> CameraTileChanged;


        protected virtual void Awake()
        {
            grid = GameObject.FindObjectOfType<HexGrid>();
            grid.MapGenerationComplete += OnMapGenerated;
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (grid.MapBuilt)
            {
                Vector2Int tileCoord = HexCoordinates.GetCartesianFromUnity(transform.position);
                if (tileCoord.x < grid.Width && tileCoord.y < grid.Height && grid[tileCoord.x, tileCoord.y] != CurrentTile)
                {
                    CurrentTile = grid[tileCoord.x, tileCoord.y];

                    CameraTileChanged(CurrentTile);
                }

            }
        }

        protected virtual void OnMapGenerated()
        {
            topRightExtent = grid[grid.Width-1, grid.Height-1].Position;
        }

        protected virtual void OnDestroy()
        {
            grid.MapGenerationComplete -= OnMapGenerated;
        }

    }
}

