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
        protected LandType[] lands;

        public override Database<LandType> GenerateDatabase()
        {
            Database<LandType> newDB = new Database<LandType>();
            string[] files = Directory.GetFiles(CSVFilePath, "*.json", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < files.Length; i++)
            {
                string jsonString = File.ReadAllText(files[i]);
                newDB.Add();
                id++;
            }

            return newDB;
        }
    }
}
