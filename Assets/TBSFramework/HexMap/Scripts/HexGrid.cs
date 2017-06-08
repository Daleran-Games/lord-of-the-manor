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

        public virtual int GetWidth { get { return cells.GetLength(0) - 1; } }
        public virtual int GetHeight { get { return cells.GetLength(0) - 1; } }

        public Action MapGenerationComplete;

        HexMesh hexMesh;
        HexCell[,] cells;
        HexOverlay overlay;
        TerrainDatabase tileDB;



        private void Awake()
        {
            hexMesh = GetComponentInChildren<HexMesh>();
            overlay = GetComponentInChildren<HexOverlay>();
            tileDB = GameDatabase.Instance.TileDB;
        }

        private void Start()
        {
            cells = Generator.GenerateMap();
            hexMesh.BuildMesh(cells, Generator.Atlas);
            overlay.CreateLabels(cells);
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
            overlay.DeleteLabels();
            cells = Generator.GenerateMap();
            hexMesh.BuildMesh(cells, Generator.Atlas);
            overlay.CreateLabels(cells);
        }


    }
}

