using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    public abstract class MapGenerator : ScriptableObject
    {

        [Header("Map Size")]
        [SerializeField]
        [Range(1, 96)]
        protected int width = 10;
        public virtual int Width { get { return width; } }
        [SerializeField]
        [Range(1,96)]
        protected int height = 10;
        public virtual int Height { get { return height; } }

        [Header("Terrain Details")]
        [SerializeField]
        protected TileAtlas atlas;
        public virtual TileAtlas Atlas { get { return atlas; } }

        [SerializeField]
        protected TerrainCuttoff[] terrainCutoffs;

        public abstract HexCell[,] GenerateMap();

        public virtual void SetTileType (HexCell hexCell)
        {
            TileType type = GameDatabase.Instance.TileDB.GetTile(terrainCutoffs[0].TileName);
            for (int i=0; i<terrainCutoffs.Length;i++)
            {
                if (hexCell.Elevation >= terrainCutoffs[i].ElevationCutoff && hexCell.Moisture >= terrainCutoffs[i].MoistureCutoff)
                     type = GameDatabase.Instance.TileDB.GetTile(terrainCutoffs[i].TileName);
            }
            hexCell.HexType = type;
            //Debug.Log("Setting cell " + hexCell.Position + " to " + type.Name);
        }

        protected virtual HexCell CreateCell(int x, int y)
        {
            Vector3 position;
            position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);
            position.y = y * (HexMetrics.outerRadius * 1.5f);
            position.z = 0f;

            return new HexCell(HexCoordinates.FromOffsetCoordinates(x, y), position);
        }

        protected virtual HexCell CreateCell(int x, int y, TileType type)
        {
            HexCell newCell = CreateCell(x, y);
            newCell.HexType = type;

            return newCell;
        }

        [System.Serializable]
        public class TerrainCuttoff
        {
            [SerializeField]
            protected string tileName;
            public string TileName { get { return tileName; } }

            [SerializeField]
            [Range(0, 255)]
            protected byte elevationCutoff = 0;
            public byte ElevationCutoff { get { return elevationCutoff; }  }


            [SerializeField]
            [Range(0, 255)]
            protected byte moistureCutoff = 0;
            public byte MoistureCutoff { get { return moistureCutoff; }  }

        }

    }
}
