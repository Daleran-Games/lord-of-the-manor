using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;
using System.IO;

namespace DaleranGames.IO
{
    [CreateAssetMenu(fileName = "GroupDatabaseLoader", menuName = "DaleranGames/Database/Groups", order = 0)]
    public class GroupsDatabaseLoader : DatabaseLoader<GroupType>
    {
        [SerializeField]
        protected List<GroupType> groups = new List<GroupType>();

        public override Database<GroupType> GenerateDatabase()
        {
            Database<GroupType> newDB = new Database<GroupType>();
            CSVData data = new CSVData("Groups", CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath)));
            groups.Clear();

            for (int i = 1; i < data.Rows; i++)
            {
                int id = Int32.Parse(data[data.FindColumnWithHeader("id"), i]);
                GroupType newType = new GroupType(data, id);
                newDB.Add(newType);
                groups.Add(newType);
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }
    }
}