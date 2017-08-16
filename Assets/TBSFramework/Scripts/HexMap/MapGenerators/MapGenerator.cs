using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

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
        [SerializeField]
        [Range(1, 32)]
        protected int playerTerritory = 5;
        public virtual int PlayerTerritory { get { return playerTerritory; } }

        [Header("Terrain Details")]
        [SerializeField]
        protected TileAtlas atlas;
        public virtual TileAtlas Atlas { get { return atlas; } }

        [SerializeField]
        [Reorderable]
        protected TerrainCuttoff[] terrainCutoffs;

        public abstract HexTile[,] GenerateMap();

        public virtual void SetTileType (HexTile hexTile)
        {
            //TODO make the tile types have a bounding box or perhaps a rect instead.

            LandType type = GameDatabase.Instance.Lands[terrainCutoffs[0].TileName];
            TerrainCuttoff initial = TerrainCuttoff.zero;
            for (int i=0; i<terrainCutoffs.Length;i++)
            {
                if (hexTile.Elevation >= terrainCutoffs[i].ElevationCutoff && hexTile.Moisture >= terrainCutoffs[i].MoistureCutoff)
                     type = GameDatabase.Instance.Lands[terrainCutoffs[i].TileName];
            }
            hexTile.Land = type;
            //Debug.Log("Setting cell " + hexCell.Position + " to " + type.Name);
        }

        protected virtual HexTile CreateTile(int x, int y, int id, float z)
        {
            Vector3 position = HexCoordinates.GetUnityPosition(x, y, z);
            return new HexTile(HexCoordinates.CartesianToHex(x, y), new Vector2Int(x,y),position, id, Atlas);
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

            public static TerrainCuttoff zero = new TerrainCuttoff("Deep Water", 0, 0);

            public TerrainCuttoff(string landName, byte elevation, byte moisture)
            {
                tileName = landName;
                elevationCutoff = elevation;
                moistureCutoff = moisture;
            }

        }

    }
}
