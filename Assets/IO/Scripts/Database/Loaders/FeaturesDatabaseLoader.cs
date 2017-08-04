using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "FeaturesDatabaseLoader", menuName = "DaleranGames/Database/Features", order = 0)]
    public class FeaturesDatabaseLoader : DatabaseLoader<FeatureType>
    {
        [SerializeField]
        protected List<FeatureType> features = new List<FeatureType>();

        public override Database<FeatureType> GenerateDatabase()
        {
            Database<FeatureType> newDB = new Database<FeatureType>();
            CSVData data = new CSVData("Improvements", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)),new List<string> { "DwellingType", "FarmType" });
            features.Clear();

            for (int i = 0; i < data.Entries; i++)
            {
                switch (data[i]["type"])
                {
                    case "DwellingType":
                        //ImprovementType newDwelling = new ImprovementType(data[i]);
                        //newDB.Add(newDwelling);
                        //improvements.Add(newDwelling);
                        break;
                    case "FarmType":
                        //ImprovementType newFarm = new ImprovementType(data[i]);
                        //newDB.Add(newFarm);
                        //improvements.Add(newFarm);
                        break;
                    default:
                        Debug.LogWarning("Database Error: " + data[i]["type"] + " not a valid type.");
                        break;
                }

            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }

    }
}
