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
            newDB.Add(new LandClearingActivity(landClearing, id));
            id++;
            newDB.Add(new RazeActivity(raze, id));
            id++;
            newDB.Add(new UpgradeActivity(upgrade, id));
            id++;

            for (int i = 0; i < builds.Length; i++)
            {
                newDB.Add(new BuildActivity(builds[i],id));
                id++;
            }

            return newDB;
        }


    }
}
