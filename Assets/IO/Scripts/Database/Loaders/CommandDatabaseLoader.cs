using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "CommandDatabaseLoader", menuName = "DaleranGames/Database/Commands", order = 0)]
    public class CommandDatabaseLoader : DatabaseLoader<Command>
    {
        [SerializeField]
        protected RazeCommand raze;
        [SerializeField]
        protected UpgradeCommand upgrade;
        [SerializeField]
        protected List<BuildCommand> builds = new List<BuildCommand>();

        public override Database<Command> GenerateDatabase()
        {
            Database<Command> newDB = new Database<Command>();
            CSVData data = new CSVData("Commands", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)));
            builds.Clear();
            raze = null;
            upgrade = null;

            for (int i = 0; i < data.Entries; i++)
            {
                switch (data[i]["type"])
                {
                    case "BuildCommand":
                        BuildCommand newBuildType = new BuildCommand(data[i]);
                        newDB.Add(newBuildType);
                        builds.Add(newBuildType);
                        break;
                    case "RazeCommand":
                        RazeCommand newRaze = new RazeCommand(data[i]);
                        raze = newRaze;
                        newDB.Add(newRaze);
                        break;
                    case "UpgradeCommand":
                        UpgradeCommand newUpgrade = new UpgradeCommand(data[i]);
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
