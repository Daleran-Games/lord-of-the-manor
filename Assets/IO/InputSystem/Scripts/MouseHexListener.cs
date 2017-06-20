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
        HexTile currentTile;
        public HexTile CurrentTile
        {
            get { return currentTile; }
            protected set
            {
                if (HexTileExited != null && CurrentTile != null)
                    HexTileExited(CurrentTile);

                currentTile = value;

                if (HexTileEntered != null)
                    HexTileEntered(CurrentTile);
            }
        }

        Vector3 mousePosition;

        Camera mainCamera;

        public Action<HexTile> HexTileLMBClicked;
        public Action<HexTile> HexTileRMBClicked;
        public Action<HexTile> HexTileMMBClicked;

        public Action<HexTile> HexTileEntered;
        public Action<HexTile> HexTileExited;

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
                Vector2Int tileCoord = HexCoordinates.GetCartesianFromUnity(mousePosition);

                //Debug.Log(cellCoord);

                if (tileCoord.x >= 0 && tileCoord.x < grid.Width && tileCoord.y >= 0 && tileCoord.y < grid.Height)
                {
                    if (grid[tileCoord.x, tileCoord.y] != CurrentTile)
                    {
                        CurrentTile = grid[tileCoord.x, tileCoord.y];
                    }
                        
                } else
                {
                    CurrentTile = null;
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
            if (HexTileLMBClicked != null && CurrentTile != null)
            {
                HexTileLMBClicked(CurrentTile);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " left clicked.");
        }

        void OnRMBClick()
        {
            if (HexTileRMBClicked != null && CurrentTile != null)
            {
                HexTileLMBClicked(CurrentTile);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " right clicked.");
        }

        void OnMMBClick()
        {
            if (HexTileMMBClicked != null && CurrentTile != null)
            {
                HexTileMMBClicked(CurrentTile);
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

