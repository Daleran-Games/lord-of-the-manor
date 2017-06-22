using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    public class HexGrid : MonoBehaviour
    {
        [SerializeField]
        MapGenerator generator;
        public MapGenerator Generator { get { return generator; } }

        [SerializeField]
        bool mapBuilt = false;
        public bool MapBuilt {get { return mapBuilt; } }

        public virtual int Width { get { return tiles.GetLength(0) ; } }
        public virtual int Height { get { return tiles.GetLength(0) ; } }

        public Action MapGenerationComplete;
        public Action MeshBuildComplete;


        [SerializeField]
        GameObject HexMeshPrefab;

        HexTile[,] tiles;

        Dictionary<HexLayers, HexMesh> meshes;

        private void Awake()
        {
            meshes = new Dictionary<HexLayers, HexMesh>();

            InstantiateHexMeshes();

            TurnManager.Instance.TurnChanged += OnTurnChange;

        }

        private void OnDestroy()
        {
            TurnManager.Instance.TurnChanged -= OnTurnChange;
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

        [ContextMenu("Generate Map")]
        public void GenerateMap ()
        {
            tiles = Generator.GenerateMap();

            if (MapGenerationComplete != null)
                MapGenerationComplete();

            foreach (KeyValuePair<HexLayers, HexMesh> kvp in meshes)
            {
                kvp.Value.BuildMesh(tiles, kvp.Key);
            }

            if (MeshBuildComplete != null)
                MeshBuildComplete();

            mapBuilt = true;
        }

        public void SwitchMaterial (Material mat)
        {
            foreach (KeyValuePair<HexLayers, HexMesh> kvp in meshes)
            {
                kvp.Value.SwitchMateiral(mat);
            }
        }

        void InstantiateHexMeshes ()
        {
            foreach (HexLayers layer in Enum.GetValues(typeof(HexLayers)))
            {
                GameObject mesh = Instantiate(HexMeshPrefab, this.transform);
                mesh.transform.position = new Vector3(transform.position.x,transform.position.y, layer.ToFloat());
                HexMesh hexMesh = mesh.GetComponent<HexMesh>();
                mesh.name = layer.ToString() + "Mesh";
                meshes.Add(layer, hexMesh);
            }
        }

        void OnTurnChange (BaseTurn newTurn)
        {
            if (newTurn is SpringTurn)
                SwitchMaterial(Generator.Atlas.SpringAtlas);
            else if (newTurn is SummerTurn)
                SwitchMaterial(Generator.Atlas.SummerAtlas);
            else if (newTurn is FallTurn)
                SwitchMaterial(Generator.Atlas.FallAtlas);
            else if (newTurn is WinterTurn)
                SwitchMaterial(Generator.Atlas.WinterAtlas);
        }


    }
}

