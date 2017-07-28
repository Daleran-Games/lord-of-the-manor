using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "ActivityDatabaseLoader", menuName = "DaleranGames/Database/Activities", order = 0)]
    public class ActivityDatabaseLoader : DatabaseLoader<Activity>
    {
        [SerializeField]
        protected RazeActivity raze;
        [SerializeField]
        protected UpgradeActivity upgrade;
        [SerializeField]
        protected List<BuildActivity> builds = new List<BuildActivity>();

        public override Database<Activity> GenerateDatabase()
        {
            Database<Activity> newDB = new Database<Activity>();
            CSVData data = new CSVData("Activities", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)));
            builds.Clear();
            raze = null;
            upgrade = null;

            for (int i = 0; i < data.Entries; i++)
            {
                switch (data[i]["type"])
                {
                    case "BuildActivity":
                        BuildActivity newBuildType = new BuildActivity(data[i]);
                        newDB.Add(newBuildType);
                        builds.Add(newBuildType);
                        break;
                    case "RazeActivity":
                        RazeActivity newRaze = new RazeActivity(data[i]);
                        raze = newRaze;
                        newDB.Add(newRaze);
                        break;
                    case "UpgradeActivity":
                        UpgradeActivity newUpgrade = new UpgradeActivity(data[i]);
                        upgrade = newUpgrade;
                        newDB.Add(newUpgrade);
                        break;
                    default:
                        Debug.LogWarning("Database Error: " + data[i]["type"] + " not a valid type.");
                        break;
                }
            }
            return newDB;
        }

    }
}
