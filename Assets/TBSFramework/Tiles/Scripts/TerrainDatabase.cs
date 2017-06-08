using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewTileDatabase", menuName = "DaleranGames/TBS/Tile Database", order = 0)]
    public class TerrainDatabase : ScriptableObject
    {

        [SerializeField]
        List<TerrainType> tiles = new List<TerrainType>();
        public List<TerrainType> Tiles { get { return tiles; } }

        public TerrainType GetTile(string name)
        {
            foreach (TerrainType t in tiles)
            {
                if (t.Name == name)
                    return t;
            }
            return null;
        }


    }
}
