using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.Database
{
    [CreateAssetMenu(fileName = "GroupDatabaseLoader", menuName = "DaleranGames/Database/Groups", order = 0)]
    public class GroupsDatabaseLoader : DatabaseLoader<GroupType>
    {
        [SerializeField]
        protected GroupType[] units;

        public override Database<GroupType> GenerateDatabase()
        {
            Database<GroupType> newDB = new Database<GroupType>();
            string[][] csvArray = CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath));

            for (int i = 1; i < csvArray.Length; i++)
            {
                newDB.Add(new GroupType());
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }
    }
}