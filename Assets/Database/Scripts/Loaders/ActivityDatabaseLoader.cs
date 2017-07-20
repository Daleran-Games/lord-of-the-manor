using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.Database
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
            string[][] csvArray = CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath));
            builds.Clear();
            raze = null;
            upgrade = null;
            landClearing = null;

            for (int i = 1; i < csvArray.Length; i++)
            {
                switch (csvArray[i][2])
                {
                    case "BuildActivity":
                        BuildActivity newBuildType = new BuildActivity(csvArray[i]);
                        newDB.Add(newBuildType);
                        builds.Add(newBuildType);
                        break;
                    case "LandClearingActivity":
                        LandClearingActivity newClear = new LandClearingActivity(csvArray[i]);
                        landClearing = newClear;
                        newDB.Add(newClear);
                        break;
                    case "RazeActivity":
                        RazeActivity newRaze = new RazeActivity(csvArray[i]);
                        raze = newRaze;
                        newDB.Add(newRaze);
                        break;
                    case "UpgradeActivity":
                        UpgradeActivity newUpgrade = new UpgradeActivity(csvArray[i]);
                        upgrade = newUpgrade;
                        newDB.Add(newUpgrade);
                        break;
                    default:
                        Debug.LogWarning("Database Error: " + csvArray[i][2] + " not a valid type.");
                        break;
                }
            }
            return newDB;
        }

    }
}
