using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.Database
{
    [CreateAssetMenu(fileName = "ImprovementsDatabaseLoader", menuName = "DaleranGames/Database/Improvements", order = 0)]
    public class ImprovementsDatabaseLoader : DatabaseLoader<ImprovementType>
    {
        [SerializeField]
        protected ImprovementType[] improvements;

        public override Database<ImprovementType> GenerateDatabase()
        {
            Database<ImprovementType> newDB = new Database<ImprovementType>();
            string[] files = Directory.GetFiles(CSVFilePath, "*.json", SearchOption.TopDirectoryOnly);

            for (int i = 0; i <files.Length; i++)
            {
                string csv = File.ReadAllText(files[i]);
                newDB.Add(new ImprovementType(JsonUtility.FromJson<ImprovementType>(csv), id));
                id++;
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }

    }
}
