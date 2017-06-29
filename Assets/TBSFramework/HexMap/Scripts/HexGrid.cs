using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    public class HexGrid : Singleton<HexGrid>
    {
        protected HexGrid() { }

        [SerializeField]
        MapGenerator generator;
        public MapGenerator Generator { get { return generator; } }

        [SerializeField]
        bool mapBuilt = false;
        public bool MapBuilt {get { return mapBuilt; } }

        [SerializeField]
        List<TileLayers> UILayers;

        public virtual int Width { get { return tiles.GetLength(0) ; } }
        public virtual int Height { get { return tiles.GetLength(0) ; } }

        public Action MapGenerationComplete;
        public Action MeshBuildComplete;


        [SerializeField]
        GameObject HexMeshPrefab;

        HexTile[,] tiles;

        Dictionary<Vector2Int,HexMeshChunk> uiMeshes;
        Dictionary<Vector2Int, HexMeshChunk> terrainMeshes;

        private void Awake()
        {
            uiMeshes = new Dictionary<Vector2Int, HexMeshChunk>();
            terrainMeshes = new Dictionary<Vector2Int, HexMeshChunk>();
        }

        private void OnDestroy()
        {
   
        }

        public virtual HexTile this[int x, int y]
        {
            get { return tiles[x, y]; }
            set
            {
                tiles[x, y] = value;
            }
        }

        public virtual HexTile this[int q, int r, int s]
        {
            get { return tiles[HexCoordinates.ToCartesianX(q, r), r]; }
            set
            {
                tiles[HexCoordinates.ToCartesianX(q,r), r] = value;
            }
        }

        public HexMeshChunk GetUIMesh (Vector2Int chunkID)
        {
            HexMeshChunk output = null;
            if (uiMeshes.TryGetValue(chunkID, out output))
            {
                return output;
            }
            return null;    
        }

        public HexMeshChunk GetTerrainMesh(Vector2Int chunkID)
        {
            HexMeshChunk output = null;
            if (terrainMeshes.TryGetValue(chunkID, out output))
            {
                return output;
            }
            return null;
        }

        public void GenerateMap ()
        {
            gameObject.transform.ClearChildren();
            uiMeshes.Clear();
            terrainMeshes.Clear();
            tiles = Generator.GenerateMap();

            if (MapGenerationComplete != null)
                MapGenerationComplete();

            InstantiateMeshChunks();

            if (MeshBuildComplete != null)
                MeshBuildComplete();

            mapBuilt = true;
            //Debug.Log(HexTile.MaxID);

        }

        void InstantiateMeshChunks ()
        {
            int chunkRows;
            int chunkColumns;

            chunkColumns = (Width / HexMetrics.xChunkSize);
            chunkRows = (Height / HexMetrics.yChunkSize);

            if (Width % HexMetrics.xChunkSize != 0)
                chunkColumns++;

            if (Height % HexMetrics.yChunkSize != 0)
                chunkRows++;

            for (int y=0; y < chunkRows; y++)
            {
                for (int x=0; x < chunkColumns; x++)
                {
                    Vector2Int chunkID = new Vector2Int(x, y);
                    GameObject newTerrainChunk = new GameObject("Terrain Chunk " + chunkID);
                    newTerrainChunk.transform.SetParent(this.transform);
                    HexMeshChunk terrainScript = newTerrainChunk.GetOrAddComponent<HexMeshChunk>();
                    terrainScript.BuildMesh(chunkID, false);
                    terrainMeshes.Add(chunkID, terrainScript);

                    GameObject newUIChunk = new GameObject("UI Chunk " + chunkID);
                    newUIChunk.transform.SetParent(this.transform);
                    HexMeshChunk uiScript = newUIChunk.GetOrAddComponent<HexMeshChunk>();
                    uiScript.BuildMesh(chunkID, true);
                    uiMeshes.Add(chunkID, terrainScript);
                }
            }
        }


    }
}

