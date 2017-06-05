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


        HexMesh hexMesh;
        HexCell[,] cells;
        HexOverlay overlay;
        TileDatabase tileDB;

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

        [ContextMenu("Regenerate Map")]
        public void RegenerateMap ()
        {
            overlay.DeleteLabels();
            cells = Generator.GenerateMap();
            hexMesh.BuildMesh(cells, Generator.Atlas);
            overlay.CreateLabels(cells);
        }


    }
}

