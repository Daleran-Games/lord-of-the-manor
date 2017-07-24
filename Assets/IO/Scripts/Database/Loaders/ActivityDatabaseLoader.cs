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
        protected LandClearingActivity landClearing;
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
            landClearing = null;

            for (int i = 1; i < data.Rows; i++)
            {
                int id = Int32.Parse(data[data.FindColumnWithHeader("id"),i]);
                switch (data["type",id])
                {
                    case "BuildActivity":
                        BuildActivity newBuildType = new BuildActivity(data,id);
                        newDB.Add(newBuildType);
                        builds.Add(newBuildType);
                        break;
                    case "LandClearingActivity":
                        LandClearingActivity newClear = new LandClearingActivity(data, id);
                        landClearing = newClear;
                        newDB.Add(newClear);
                        break;
                    case "RazeActivity":
                        RazeActivity newRaze = new RazeActivity(data, id);
                        raze = newRaze;
                        newDB.Add(newRaze);
                        break;
                    case "UpgradeActivity":
                        UpgradeActivity newUpgrade = new UpgradeActivity(data, id);
                        upgrade = newUpgrade;
                        newDB.Add(newUpgrade);
                        break;
                    default:
                        Debug.LogWarning("Database Error: " + data["type", id] + " not a valid type.");
                        break;
                }
            }
            return newDB;
        }

    }
}
