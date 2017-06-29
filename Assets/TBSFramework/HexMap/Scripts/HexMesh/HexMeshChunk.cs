using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class HexMeshChunk : MonoBehaviour
    {
        Mesh hexMesh;
        MeshRenderer meshRenderer;
        TileAtlas meshAtlas;
        HexGrid grid;

        [SerializeField]
        bool meshBuilt = false;
        public bool MeshBuilt { get { return meshBuilt; } protected set { meshBuilt = value; } }

        [SerializeField]
        bool uiMesh = false;
        public bool UIMesh { get { return uiMesh; } protected set { uiMesh = value; } }

        [SerializeField]
        Vector2Int chunk;
        public Vector2Int Chunk { get { return chunk; } protected set { chunk = value; } }


        MeshData meshData;
        Vector2Int minTile;
        Vector2Int maxTile;
        
        void Awake()
        {
            hexMesh = gameObject.GetOrAddComponent<MeshFilter>().mesh = new Mesh();
            meshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
            TurnManager.Instance.TurnChanged += OnTurnChange;
            grid = GetComponentInParent<HexGrid>();
            meshAtlas = grid.Generator.Atlas;

            hexMesh.MarkDynamic();

        }

        private void OnDestroy()
        {
            TurnManager.Instance.TurnChanged -= OnTurnChange;

            for (int y = minTile.y; y <= maxTile.y; y++)
            {
                for (int x = minTile.x; x <= maxTile.x; x++)
                {
                    if (UIMesh)
                        grid[x, y].UIGraphics.GraphicsChanged -= OnTileChange;
                    else
                        grid[x, y].TerrainGraphics.GraphicsChanged -= OnTileChange;
                }
            }
        }

        public void BuildMesh(Vector2Int chunk, bool ui)
        {
            UIMesh = ui;
            Chunk = chunk;
            meshData = new MeshData();

            minTile = new Vector2Int(Chunk.x * HexMetrics.xChunkSize, Chunk.y * HexMetrics.yChunkSize);

            int maxX;
            int maxY;

            if (grid.Width <= minTile.x + HexMetrics.xChunkSize)
                maxX = grid.Width - 1;
            else
                maxX = minTile.x + HexMetrics.xChunkSize;

            if (grid.Height <= minTile.y + HexMetrics.yChunkSize)
                maxY = grid.Height - 1;
            else
                maxY = minTile.y + HexMetrics.yChunkSize;

            maxTile = new Vector2Int(maxX, maxY);

            if (UIMesh)
            {
                meshRenderer.material = meshAtlas.UIAtlas;
            } else
            {
                OnTurnChange(TurnManager.Instance.CurrentTurn);
            }

            for (int y = minTile.y; y <= maxTile.y; y++)
            {
                for (int x = minTile.x; x <= maxTile.x; x++)
                {
                    if (UIMesh)
                        grid[x, y].UIGraphics.GraphicsChanged += OnTileChange;
                    else
                        grid[x, y].TerrainGraphics.GraphicsChanged += OnTileChange;
                }
            }

            MeshBuilt = true;
        }

        public void RefreshMesh()
        {
            meshData.Clear();
            hexMesh.Clear();

            for (int y = minTile.y; y <= maxTile.y; y++)
            {
                for (int x = minTile.x; x <= maxTile.x; x++)
                {
                    if (UIMesh)
                        meshData += grid[x, y].UIGraphics.MeshData;
                    else
                        meshData += grid[x, y].TerrainGraphics.MeshData;
                }
            }

            hexMesh.vertices = meshData.verticies.ToArray();
            hexMesh.triangles = meshData.triangles.ToArray();
            hexMesh.uv = meshData.uvs.ToArray();
            hexMesh.RecalculateNormals();

        }

        void OnTileChange()
        {
            enabled = true;
        }

        private void LateUpdate()
        {

            RefreshMesh();
            enabled = false;

        }

        void OnTurnChange (BaseTurn newTurn)
        {
            if (!UIMesh)
            {
                if (newTurn is SpringTurn)
                    meshRenderer.material = meshAtlas.SpringAtlas;
                else if (newTurn is SummerTurn)
                    meshRenderer.material = meshAtlas.SummerAtlas;
                else if (newTurn is FallTurn)
                    meshRenderer.material = meshAtlas.FallAtlas;
                else if (newTurn is WinterTurn)
                    meshRenderer.material = meshAtlas.WinterAtlas;
            }

        }

    }
}
