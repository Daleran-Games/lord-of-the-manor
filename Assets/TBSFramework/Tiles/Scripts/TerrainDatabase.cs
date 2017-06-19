using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewTileDatabase", menuName = "DaleranGames/TBS/Tile Database", order = 0)]
    public class TerrainDatabase : ScriptableObject
    {

        [SerializeField]
        List<LandType> tiles = new List<LandType>();
        public List<LandType> Tiles { get { return tiles; } }

        public LandType GetTile(string name)
        {
            foreach (LandType t in tiles)
            {
                if (t.Name == name)
                    return t;
            }
            return null;
        }


    }
}
