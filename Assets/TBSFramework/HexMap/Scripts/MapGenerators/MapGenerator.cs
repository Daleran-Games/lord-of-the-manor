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

        public abstract HexTile[,] GenerateMap();

        public virtual void SetTileType (HexTile hexTile)
        {
            LandType type = GameDatabase.Instance.GetDatabaseObject<LandType>(terrainCutoffs[0].TileName);
            for (int i=0; i<terrainCutoffs.Length;i++)
            {
                if (hexTile.Elevation >= terrainCutoffs[i].ElevationCutoff && hexTile.Moisture >= terrainCutoffs[i].MoistureCutoff)
                     type = GameDatabase.Instance.GetDatabaseObject<LandType>(terrainCutoffs[i].TileName);
            }
            hexTile.Land = type;
            //Debug.Log("Setting cell " + hexCell.Position + " to " + type.Name);
        }

        protected virtual HexTile CreateTile(int x, int y, int id)
        {
            Vector3 position = HexCoordinates.GetUnityPosition(x, y);
            return new HexTile(HexCoordinates.CartesianToHex(x, y), position, id);
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
