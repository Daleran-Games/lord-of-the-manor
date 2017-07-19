﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DaleranGames.Database
{
    public abstract class DatabaseLoader<T> : ScriptableObject where T: IDatabaseObject
    {
        [SerializeField]
        string csvFilePath;
        protected string CSVFilePath { get { return "GameData/"+csvFilePath + "/"; } }

        public abstract Database<T> GenerateDatabase();

        public virtual void InitializeDatabase(Database<T> newDB)
        {
            foreach (KeyValuePair<int, T> kvp in newDB.Dict)
            {
                kvp.Value.OnDatabaseInitialization();
            }
        }

    }
}
