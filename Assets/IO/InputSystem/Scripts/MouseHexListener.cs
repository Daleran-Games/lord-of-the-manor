using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;

namespace DaleranGames.IO
{
    public class MouseHexListener : MonoBehaviour
    {

        HexGrid grid;
        
        [SerializeField]
        bool mapBuilt = false;

        [SerializeField]
        HexCell currentCell;
        public HexCell CurrentCell
        {
            get { return currentCell; }
            protected set
            {
                if (HexCellExited != null && CurrentCell != null)
                    HexCellExited(CurrentCell);

                currentCell = value;

                if (HexCellEntered != null)
                    HexCellEntered(CurrentCell);
            }
        }

        Vector3 mousePosition;

        Camera mainCamera;

        public Action<HexCell> HexCellLMBClicked;
        public Action<HexCell> HexCellRMBClicked;
        public Action<HexCell> HexCellMMBClicked;

        public Action<HexCell> HexCellEntered;
        public Action<HexCell> HexCellExited;

        private void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            grid.MapGenerationComplete += OnMapGenerationComplete;
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetRequiredComponent<Camera>();
        }

        private void Update()
        {
            if (mapBuilt)
            {
                mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector2Int cellCoord = HexCoordinates.GetCartesianFromUnity(mousePosition);

                //Debug.Log(cellCoord);

                if (cellCoord.x >= 0 && cellCoord.x < grid.Width && cellCoord.y >= 0 && cellCoord.y < grid.Height)
                {
                    if (grid[cellCoord.x, cellCoord.y] != CurrentCell)
                    {
                        CurrentCell = grid[cellCoord.x, cellCoord.y];
                    }
                        
                } else
                {
                    CurrentCell = null;
                }

            }

            

        }

        private void OnDestroy()
        {
            grid.MapGenerationComplete -= OnMapGenerationComplete;
            InputManager.Instance.LMBClick.MouseButtonUp -= OnLMBClick;
            InputManager.Instance.RMBClick.MouseButtonUp -= OnRMBClick;
            InputManager.Instance.MMBClick.MouseButtonUp -= OnMMBClick;
        }

        void OnLMBClick ()
        {
            if (HexCellLMBClicked != null && CurrentCell != null)
            {
                HexCellLMBClicked(CurrentCell);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " left clicked.");
        }

        void OnRMBClick()
        {
            if (HexCellRMBClicked != null && CurrentCell != null)
            {
                HexCellLMBClicked(CurrentCell);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " right clicked.");
        }

        void OnMMBClick()
        {
            if (HexCellMMBClicked != null && CurrentCell != null)
            {
                HexCellMMBClicked(CurrentCell);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " middle clicked.");
        }

        void OnMapGenerationComplete()
        {
            mapBuilt = true;

            InputManager.Instance.LMBClick.MouseButtonUp += OnLMBClick;
            InputManager.Instance.RMBClick.MouseButtonUp += OnRMBClick;
            InputManager.Instance.MMBClick.MouseButtonUp += OnMMBClick;

        }

       
    }
}

