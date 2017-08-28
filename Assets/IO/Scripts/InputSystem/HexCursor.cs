using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DaleranGames.TBSFramework;
using DaleranGames.IO;

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
#pragma warning disable 0649
        [SerializeField]
        GameObject HexQuadPrefab;
#pragma warning restore 0649

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
                        SetSelectionCursor(TileGraphic.Clear);
                        break;
                    case HexCursorMode.Cross:
                        hexCursorMode = value;
                        SetSelectionCursor(cross);
                        break;
                    default:
                        hexCursorMode = HexCursorMode.Clear;
                        SetSelectionCursor(TileGraphic.Clear);
                        break;

                }
            }
        }

        [SerializeField]
        [ReadOnly]
        TileGraphic cursorUIIcon = TileGraphic.Clear;
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
        TileGraphic cursorTerrainIcon = TileGraphic.Clear;
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
                if (CurrentTile != null)
                    HexTileExited(CurrentTile);

                currentTile = value;
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


        public event Action<HexTile> HexTileLMBClicked;
        public event Action<HexTile> HexTileRMBClicked;
        public event Action<HexTile> HexTileMMBClicked;

        public event Action<HexTile> HexTileEntered;
        public event Action<HexTile> HexTileExited;

        private void Awake()
        {
            grid = FindObjectOfType<HexGrid>();
            grid.MapGenerationComplete += OnMapGenerationComplete;
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetRequiredComponent<Camera>();
            atlas = grid.Generator.Atlas;

            BuildQuads();

        }

        private void Start()
        {
            ring = GameDatabase.Instance.TileGraphics[ringName];
            dark = GameDatabase.Instance.TileGraphics[darkName];
            white = GameDatabase.Instance.TileGraphics[whiteName];
            positive = GameDatabase.Instance.TileGraphics[positiveName];
            negative = GameDatabase.Instance.TileGraphics[negativeName];
            cross = GameDatabase.Instance.TileGraphics[crossName];

            CursorMode = HexCursorMode.Ring;
        }

        private void Update()
        {
            if (mapBuilt)
            {
                mousePosition = MouseCursor.Instance.WorldPosition;
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
            MouseCursor.Instance.LMBClick.MouseButtonUp -= OnLMBClick;
            MouseCursor.Instance.RMBClick.MouseButtonUp -= OnRMBClick;
            MouseCursor.Instance.MMBClick.MouseButtonUp -= OnMMBClick;
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

            MouseCursor.Instance.LMBClick.MouseButtonUp += OnLMBClick;
            MouseCursor.Instance.RMBClick.MouseButtonUp += OnRMBClick;
            MouseCursor.Instance.MMBClick.MouseButtonUp += OnMMBClick;

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

