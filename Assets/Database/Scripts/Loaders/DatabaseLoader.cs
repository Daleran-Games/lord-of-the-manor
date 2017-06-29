﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Database
{
    public abstract class DatabaseLoader<T> : MonoBehaviour where T: IDatabaseObject
    {
        [SerializeField]
        string filePath;
        protected string FilePath { get { return Application.dataPath + filePath; } }

        public abstract Database<T> GenerateDatabase();
        public abstract void InitializeDatabase(Database<T> newDB);

    }
}
