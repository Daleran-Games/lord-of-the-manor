using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DaleranGames.TBSFramework;
using DaleranGames.Tools;

namespace DaleranGames.IO
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
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
            Clear
        }

        [SerializeField]
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
                        SetCursor(ring, true);
                        break;
                    case HexCursorMode.Dark:
                        hexCursorMode = value;
                        SetCursor(dark, true);
                        break;
                    case HexCursorMode.White:
                        hexCursorMode = value;
                        SetCursor(white, true);
                        break;
                    case HexCursorMode.Positive:
                        hexCursorMode = value;
                        SetCursor(positive, true);
                        break;
                    case HexCursorMode.Negative:
                        hexCursorMode = value;
                        SetCursor(negative, true);
                        break;
                    case HexCursorMode.Clear:
                        hexCursorMode = value;
                        SetCursor(Vector2Int.zero, true);
                        break;
                    default:
                        hexCursorMode = HexCursorMode.Clear;
                        SetCursor(Vector2Int.zero, true);
                        break;

                }
            }
        }

        [SerializeField]
        [ReadOnly]
        Vector2Int cursorIcon = Vector2Int.zero;
        public Vector2Int CursorIcon
        {
            get { return cursorIcon; }
            set
            {
                cursorIcon = value;
                SetCursor(cursorIcon, false);
            }
        }

        [Header("Selection Tiles")]

        [SerializeField]
        Vector2Int ring;

        [SerializeField]
        Vector2Int dark;

        [SerializeField]
        Vector2Int white;

        [SerializeField]
        Vector2Int positive;

        [SerializeField]
        Vector2Int negative;


        [Header("Selection Tiles")]
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
        Mesh cursorMesh;
        MeshRenderer meshRenderer;
        List<Vector3> vertices;
        List<int> triangles;
        List<Vector2> uvs;


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
            meshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
            meshRenderer.material = atlas.SpringAtlas;

            //cursor = GameObject.FindObjectOfType<MouseCursor>();

            GetComponent<MeshFilter>().mesh = cursorMesh = new Mesh();
            vertices = new List<Vector3>();
            triangles = new List<int>();
            uvs = new List<Vector2>();

            BuildMesh();

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
                        transform.position = CurrentTile.Position;
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
        }

        void OnLMBClick ()
        {
            if (HexTileLMBClicked != null && CurrentTile != null && !EventSystem.current.IsPointerOverGameObject())
            {
                    HexTileLMBClicked(CurrentTile);
            }
            //Debug.Log(CurrentCell.HexTerrainType.Name + " at " + CurrentCell.Coord.ToString() + " left clicked.");
        }

        void OnRMBClick()
        {
            if (HexTileRMBClicked != null && CurrentTile != null && !EventSystem.current.IsPointerOverGameObject())
            {
                    HexTileLMBClicked(CurrentTile);
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

        void BuildMesh()
        {
            cursorMesh.Clear();
            vertices.Clear();
            triangles.Clear();
            uvs.Clear();
            cursorMesh.MarkDynamic();

            Vector3 selectionPos = transform.position + new Vector3(0f, 0f, -9.1f);
            Vector3 iconPos = transform.position + new Vector3(0f, 0f, -9.2f);

            vertices.AddRange(HexMetrics.CalculateVerticies(iconPos));
            vertices.AddRange(HexMetrics.CalculateVerticies(selectionPos));

            triangles.AddRange(HexMetrics.CalculateTriangles(0));
            triangles.AddRange(HexMetrics.CalculateTriangles(4));

            uvs.AddRange(atlas.CalculateUVs(Vector2Int.zero));
            uvs.AddRange(atlas.CalculateUVs(ring));


            cursorMesh.vertices = vertices.ToArray();
            cursorMesh.triangles = triangles.ToArray();
            cursorMesh.uv = uvs.ToArray();
            cursorMesh.RecalculateNormals();

        }

        void SetCursor (Vector2Int coord, bool cursorOrIcon)
        {
            Vector2[] newUV = atlas.CalculateUVs(coord);
            int i = 0;

            if (cursorOrIcon == true)
                i = 4;

            uvs[i] = newUV[0];
            uvs[i + 1] = newUV[1];
            uvs[i + 2] = newUV[2];
            uvs[i + 3] = newUV[3];
            cursorMesh.uv = uvs.ToArray();
        }

       
    }
}

