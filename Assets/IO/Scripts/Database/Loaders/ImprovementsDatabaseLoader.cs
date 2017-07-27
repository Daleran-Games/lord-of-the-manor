using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "ImprovementsDatabaseLoader", menuName = "DaleranGames/Database/Improvements", order = 0)]
    public class ImprovementsDatabaseLoader : DatabaseLoader<ImprovementType>
    {
        [SerializeField]
        protected List<ImprovementType> improvements = new List<ImprovementType>();

        public override Database<ImprovementType> GenerateDatabase()
        {
            Database<ImprovementType> newDB = new Database<ImprovementType>();
            CSVData data = new CSVData("Improvements", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)),new List<string> { "DwellingType", "FarmType" });
            improvements.Clear();

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
