using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewTileDatabase", menuName = "DaleranGames/TBS/Tile Database", order = 0)]
    public class TileDatabase : ScriptableObject
    {

        [SerializeField]
        List<TileType> tiles = new List<TileType>();
        public List<TileType> Tiles { get { return tiles; } }

        public TileType GetTile(string name)
        {
            foreach (TileType t in tiles)
            {
                if (t.Name == name)
                    return t;
            }
            return null;
        }


    }
}
