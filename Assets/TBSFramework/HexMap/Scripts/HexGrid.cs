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
        bool isMapBuilt = false;
        public bool IsMapBuilt {get { return isMapBuilt; } }

        public virtual int Width { get { return cells.GetLength(0) ; } }
        public virtual int Height { get { return cells.GetLength(0) ; } }

        public Action MapGenerationComplete;
        public Action MeshBuildComplete;

        HexMesh hexMesh;
        HexCell[,] cells;
        TerrainDatabase tileDB;



        private void Awake()
        {
            hexMesh = GetComponentInChildren<HexMesh>();
            tileDB = GameDatabase.Instance.TileDB;

            TurnManager.Instance.TurnChanged += OnTurnChange;

        }

        private void OnDestroy()
        {
            TurnManager.Instance.TurnChanged -= OnTurnChange;
        }

        public virtual HexCell this[int x, int y]
        {
            get { return cells[x, y]; }
            set
            {
                cells[x, y] = value;
            }
        }

        public virtual HexCell this[int q, int r, int s]
        {
            get { return cells[HexCoordinates.ToCartesianX(q, r), r]; }
            set
            {
                cells[HexCoordinates.ToCartesianX(q,r), r] = value;
            }
        }

        [ContextMenu("Generate Map")]
        public void GenerateMap ()
        {
            cells = Generator.GenerateMap();

            if (MapGenerationComplete != null)
                MapGenerationComplete();

            hexMesh.BuildMesh(cells, Generator.Atlas);

            if (MeshBuildComplete != null)
                MeshBuildComplete();

            isMapBuilt = true;
        }

        public void SwitchMaterial (Material mat)
        {
            hexMesh.SwitchMateiral(mat);
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

