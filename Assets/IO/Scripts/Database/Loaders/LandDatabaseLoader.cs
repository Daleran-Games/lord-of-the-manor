using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "LandDatabaseLoader", menuName = "DaleranGames/Database/Land", order = 0)]
    public class LandDatabaseLoader : DatabaseLoader<LandType>
    {
        [SerializeField]
        protected List<LandType> lands = new List<LandType>();

        public override Database<LandType> GenerateDatabase()
        {
            Database<LandType> newDB = new Database<LandType>();
            CSVData data = new CSVData("Lands", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)));
            lands.Clear();

            for (int i = 0; i < data.Entries; i++)
            {
                LandType newType = new LandType(data[i]);
                newDB.Add(newType);
                lands.Add(newType);
            }

            return newDB;
        }

    }
}
