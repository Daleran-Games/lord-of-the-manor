using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using System;

namespace DaleranGames.Database
{
    public class ImprovementsDatabaseLoader : DatabaseLoader<ImprovementType>
    {
        [SerializeField]
        [Reorderable]
        protected ImprovementType[] improvements;

        public override Database<ImprovementType> GenerateDatabase()
        {
            Database<ImprovementType> newDB = new Database<ImprovementType>();
            for (int i = 0; i < improvements.Length; i++)
            {
                newDB.Add(new ImprovementType(improvements[i],id));
                id++;
            }
            //Debug.Log("Improvment types types created");
            return newDB;

        }


    }
}
