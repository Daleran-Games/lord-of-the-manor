using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;
using DaleranGames.TBSFramework;

namespace DaleranGames.Database
{
    public class GameDatabase : Singleton<GameDatabase>
    {

        protected GameDatabase() { }

        [SerializeField]
        TerrainDatabase tileDB;
        public TerrainDatabase TileDB { get { return tileDB; } }

        
    }
}

