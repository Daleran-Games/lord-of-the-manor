using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.Database
{
    [CreateAssetMenu(fileName = "UnitDatabaseLoader", menuName = "DaleranGames/Database/Units", order = 0)]
    public class UnitsDatabaseLoader : DatabaseLoader<GroupType>
    {
        [SerializeField]
        [Reorderable]
        protected GroupType[] units;

        public override Database<GroupType> GenerateDatabase()
        {
            Database<GroupType> newDB = new Database<GroupType>();
            string[] files = Directory.GetFiles(JSONFilePath, "*.json", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < files.Length; i++)
            {
                string jsonString = File.ReadAllText(files[i]);
                newDB.Add(new GroupType(JsonUtility.FromJson<GroupType>(jsonString), id));
                id++;
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }
    }
}