using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.Database
{
    [CreateAssetMenu(fileName = "LandDatabaseLoader", menuName = "DaleranGames/Database/Land", order = 0)]
    public class LandDatabaseLoader : DatabaseLoader<LandType>
    {
        [SerializeField]
        protected List<LandType> lands = new List<LandType>();

        public override Database<LandType> GenerateDatabase()
        {
            Database<LandType> newDB = new Database<LandType>();
            string[][] csvArray = CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath));
            lands.Clear();

            for (int i = 1; i < csvArray.Length; i++)
            {
                LandType newType = new LandType(csvArray[i]);
                newDB.Add(newType);
                lands.Add(newType);
            }

            return newDB;
        }

    }
}
