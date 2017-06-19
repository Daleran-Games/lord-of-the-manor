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
        HexCell currentCell;
        public HexCell CurrentCell
        {
            get { return currentCell; }
            protected set
            {
                currentCell = value;

            }
        }

        public event System.Action<HexCell> CameraCellChanged;


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

            if (grid.IsMapBuilt)
            {
                Vector2Int cellCoord = HexCoordinates.GetCartesianFromUnity(transform.position);
                if (cellCoord.x < grid.Width && cellCoord.y < grid.Height && grid[cellCoord.x, cellCoord.y] != CurrentCell)
                {
                    CurrentCell = grid[cellCoord.x, cellCoord.y];

                    if (CameraCellChanged != null)
                        CameraCellChanged(CurrentCell);
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

