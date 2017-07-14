using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DaleranGames.TBSFramework;
using DaleranGames.Database;

namespace DaleranGames.IO
{
    public class HexCursor : Singleton<HexCursor>
    {
        protected HexCursor( ) { }

        public enum HexCursorMode
        {
            Ring,
            Dark,
            White,
            Positive,
            Negative,
            Clear,
            Cross
        }

        [SerializeField]
        GameObject HexQuadPrefab;

        [Header("Cursor Modes")]
        [SerializeField]
        [ReadOnly]
        HexCursorMode hexCursorMode = HexCursorMode.Ring;
        public HexCursorMode CursorMode
        {
            get { return hexCursorMode; }
            set
            {
                switch(value)
                {
                    case HexCursorMode.Ring:
                        hexCursorMode = value;
                        SetSelectionCursor(ring);
                        break;
                    case HexCursorMode.Dark:
                        hexCursorMode = value;
                        SetSelectionCursor(dark);
                        break;
                    case HexCursorMode.White:
                        hexCursorMode = value;
                        SetSelectionCursor(white);
                        break;
                    case HexCursorMode.Positive:
                        hexCursorMode = value;
                        SetSelectionCursor(positive);
                        break;
                    case HexCursorMode.Negative:
                        hexCursorMode = value;
                        SetSelectionCursor(negative);
                        break;
                    case HexCursorMode.Clear:
                        hexCursorMode = value;
                        SetSelectionCursor(TileGraphic.clear);
                        break;
                    case HexCursorMode.Cross:
                        hexCursorMode = value;
                        SetSelectionCursor(cross);
                        break;
                    default:
                        hexCursorMode = HexCursorMode.Clear;
                        SetSelectionCursor(TileGraphic.clear);
                        break;

                }
            }
        }

        [SerializeField]
        [ReadOnly]
        TileGraphic cursorUIIcon = TileGraphic.clear;
        public TileGraphic CursorUIIcon
        {
            get { return cursorUIIcon; }
            set
            {
                cursorUIIcon = value;
                SetUIIconCursor(cursorUIIcon);
            }
        }

        [SerializeField]
        [ReadOnly]
        TileGraphic cursorTerrainIcon = TileGraphic.clear;
        public TileGraphic CursorTerrainIcon
        {
            get { return cursorTerrainIcon; }
            set
            {
                cursorTerrainIcon = value;
                SetTerrainIconCursor(cursorTerrainIcon);
            }
        }

        [Header("Cursor Modes")]
        [SerializeField]
        string ringName = "UI Cursor Ring";
        TileGraphic ring;

        [SerializeField]
        string darkName = "UI Cursor Dark";
        TileGraphic dark;

        [SerializeField]
        string whiteName = "UI Cursor White";
        TileGraphic white;

        [SerializeField]
        string positiveName = "UI Cursor Positive";
        TileGraphic positive;

        [SerializeField]
        string negativeName = "UI Cursor Negative";
        TileGraphic negative;

        [SerializeField]
        string crossName = "UI Cursor Cross";
        TileGraphic cross;


        [Header("Current Tile")]
        [SerializeField]
        [ReadOnly]
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
        HexGrid grid;
        Camera mainCamera;
        //MouseCursor cursor;
        bool mapBuilt = false;

        TileAtlas atlas;

        HexQuad highlightQuad;
        HexQuad uiIconQuad;
        HexQuad terrainIconQuad;


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
            atlas = grid.Generator.Atlas;
            GameDatabase.Instance.DatabasesInitialized += OnDatabaseComplete;

            BuildQuads();

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
                        transform.position = new Vector3(CurrentTile.Position.x,CurrentTile.Position.y,transform.position.z);
                        //cursor.ToggleCursor(false);
                    }
                        
                } else
                {
                    CurrentTile = null;
                    //cursor.ToggleCursor(true);
                }

                //if (EventSystem.current.IsPointerOverGameObject())
                    //cursor.ToggleCursor(true);

            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            grid.MapGenerationComplete -= OnMapGenerationComplete;
            InputManager.Instance.LMBClick.MouseButtonUp -= OnLMBClick;
            InputManager.Instance.RMBClick.MouseButtonUp -= OnRMBClick;
            InputManager.Instance.MMBClick.MouseButtonUp -= OnMMBClick;
            GameDatabase.Instance.DatabasesInitialized -= OnDatabaseComplete;
        }

        void OnDatabaseComplete()
        {
            ring = GameDatabase.Instance.TileGraphics[ringName];
            dark = GameDatabase.Instance.TileGraphics[darkName];
            white = GameDatabase.Instance.TileGraphics[whiteName];
            positive = GameDatabase.Instance.TileGraphics[positiveName];
            negative = GameDatabase.Instance.TileGraphics[negativeName];
            cross = GameDatabase.Instance.TileGraphics[crossName];

            CursorMode = HexCursorMode.Ring;
        }

        void OnLMBClick ()
        {
            if (HexTileLMBClicked != null && CurrentTile != null && !EventSystem.current.IsPointerOverGameObject())
            {
                    HexTileLMBClicked(CurrentTile);
            }
            //Debug.Log(CurrentTile.Land.name + " at " + CurrentTile.Coord.ToString() + " left clicked.");
        }

        void OnRMBClick()
        {
            if (HexTileRMBClicked != null && CurrentTile != null && !EventSystem.current.IsPointerOverGameObject())
            {
                    HexTileRMBClicked(CurrentTile);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " right clicked.");
        }

        void OnMMBClick()
        {
            if (HexTileMMBClicked != null && CurrentTile != null && !EventSystem.current.IsPointerOverGameObject())
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

        bool IsClickOnUIElement ()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, 100f, LayerMask.NameToLayer("UI")))
            {
                return true;
            }
            else
                return false;
        }

        void BuildQuads()
        {

            Vector3 selectionPos = transform.position + new Vector3(0f, 0f, -9.3f);
            Vector3 uiIconPos = transform.position + new Vector3(0f, 0f, -9.2f);
            Vector3 terrainIconPos = transform.position + new Vector3(0f, 0f, -9.1f);

            highlightQuad = InstantiateHexQuad(selectionPos, atlas.UIAtlas);
            uiIconQuad = InstantiateHexQuad(uiIconPos, atlas.UIAtlas);
            terrainIconQuad = InstantiateHexQuad(terrainIconPos, atlas.SpringAtlas);



        }

        HexQuad InstantiateHexQuad(Vector3 position, Material mat)
        {
            GameObject mesh = Instantiate(HexQuadPrefab, this.transform);
            HexQuad hexQuad = mesh.GetComponent<HexQuad>();
            hexQuad.BuildQuad(position, atlas, mat);

            return hexQuad;
        }

        void SetSelectionCursor (TileGraphic graphic)
        {
            highlightQuad.SetUV(graphic.AtlasCoord);
        }

        void SetUIIconCursor (TileGraphic graphic)
        {
            uiIconQuad.SetUV(graphic.AtlasCoord);
        }

        void SetTerrainIconCursor(TileGraphic graphic)
        {
            terrainIconQuad.SetUV(graphic.AtlasCoord);
        }


    }
}

