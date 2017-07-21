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
            string[][] csvArray = CSVUtility.ParseCSVToArray(File.ReadAllText(CSVFilePath));
            improvements.Clear();

            for (int i = 1; i < csvArray.Length; i++)
            {
                ImprovementType newType = new ImprovementType(csvArray[i]);
                newDB.Add(newType);
                improvements.Add(newType);
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }

    }
}
