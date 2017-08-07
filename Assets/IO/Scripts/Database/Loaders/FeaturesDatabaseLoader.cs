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
        public List<BuildFeature> buildFeatures = new List<BuildFeature>();
        public List<DwellingFeature> dwellingFeatures = new List<DwellingFeature>();
        public List<FarmFeature> farmFeatures = new List<FarmFeature>();
        public List<LoggingFeature> loggingFeatures = new List<LoggingFeature>();


        public override Database<FeatureType> GenerateDatabase()
        {
            Database<FeatureType> newDB = new Database<FeatureType>();
            CSVData data = new CSVData("Features", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)),new List<string> { "DwellingType", "FarmType", "LoggingType", "QuarryType" });
            buildFeatures.Clear();
            dwellingFeatures.Clear();

            for (int i = 0; i < data.Entries; i++)
            {
                switch (data[i]["type"])
                {
                    case "DwellingType":
                        DwellingFeature newDwelling = new DwellingFeature(data[i]);
                        newDB.Add(newDwelling);
                        dwellingFeatures.Add(newDwelling);
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
