using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;

namespace DaleranGames.Database
{
    public class ActivityDatabaseLoader : DatabaseLoader<Activity>
    {
        [SerializeField]
        protected LandClearingActivity landClearing;
        [SerializeField]
        protected RazeActivity raze;
        [SerializeField]
        protected UpgradeActivity upgrade;
        [SerializeField]
        [Reorderable]
        protected BuildActivity[] builds;

        public override Database<Activity> GenerateDatabase()
        {
            Database<Activity> newDB = new Database<Activity>();
            int id = 0;
            newDB.Add(new LandClearingActivity(landClearing.Name, id));
            id++;
            newDB.Add(new RazeActivity(raze.Name, id));
            id++;
            newDB.Add(new UpgradeActivity(upgrade.Name, id));
            id++;

            for (int i = 0; i < builds.Length; i++)
            {
                newDB.Add(new BuildActivity(builds[i].Name,id,builds[i].ImprovementName));
                id++;
            }

            return newDB;
        }


    }
}
