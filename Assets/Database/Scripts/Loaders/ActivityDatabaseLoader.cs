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
        protected BuildActivity[] builds;

        public override Database<Activity> GenerateDatabase()
        {
            Database<Activity> newDB = new Database<Activity>();
            string[] files = Directory.GetFiles(CSVFilePath, "*.json", SearchOption.TopDirectoryOnly);

            for (int i = 0; i < files.Length; i++)
            {
                string jsonString = File.ReadAllText(files[i]);
                Activity activity = JsonUtility.FromJson<Activity>(jsonString);

                switch(activity.Type)
                {
                    case "DaleranGames.TBSFramework.BuildActivity":
                        newDB.Add();
                        break;
                    case "DaleranGames.TBSFramework.LandClearingActivity":
                        newDB.Add(new LandClearingActivity(JsonUtility.FromJson<LandClearingActivity>(jsonString), id));
                        break;
                    case "DaleranGames.TBSFramework.RazeActivity":
                        newDB.Add(new RazeActivity(JsonUtility.FromJson<RazeActivity>(jsonString), id));
                        break;
                    case "DaleranGames.TBSFramework.UpgradeActivity":
                        newDB.Add(new UpgradeActivity(JsonUtility.FromJson<UpgradeActivity>(jsonString), id));
                        break;
                    default:
                        Debug.LogError("Database Error: "+activity.Type+ " not a valid type.");
                        break;
                }

            }

            return newDB;
        }

    }
}
