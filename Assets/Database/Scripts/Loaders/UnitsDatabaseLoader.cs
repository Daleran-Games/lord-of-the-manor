using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.Database
{
    public class UnitsDatabaseLoader : DatabaseLoader<UnitType>
    {
        [SerializeField]
        [Reorderable]
        protected UnitType[] units;

        public override Database<UnitType> GenerateDatabase()
        {
            Database<UnitType> newDB = new Database<UnitType>();
            string[] files = Directory.GetFiles(JSONFilePath, "*.json", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < files.Length; i++)
            {
                string jsonString = File.ReadAllText(files[i]);
                newDB.Add(new UnitType(JsonUtility.FromJson<UnitType>(jsonString), id));
                id++;
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }

        public override void BuildJSONFiles()
        {
            Directory.CreateDirectory(JSONFilePath);

            for (int i = 0; i < units.Length; i++)
            {
                File.WriteAllText(JSONFilePath + units[i].Name + ".json", units[i].ToJson());
            }
        }


    }
}