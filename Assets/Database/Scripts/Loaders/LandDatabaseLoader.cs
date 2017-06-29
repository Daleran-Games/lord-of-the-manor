using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;

namespace DaleranGames.Database
{
    public class LandDatabaseLoader : DatabaseLoader<LandType>
    {
        [SerializeField]
        [Reorderable]
        protected LandType[] lands;

        public override Database<LandType> GenerateDatabase()
        {
            Database<LandType> newDB = new Database<LandType>();
            for (int i = 0; i < lands.Length; i++)
            {
                newDB.Add(new LandType(lands[i].Name, i, lands[i].IconName, lands[i].ClearedLandName ,lands[i].Clearable));
            }


            return newDB;
        }

    }
}
