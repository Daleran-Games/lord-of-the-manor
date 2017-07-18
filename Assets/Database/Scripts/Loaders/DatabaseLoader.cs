using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace DaleranGames.Database
{
    public abstract class DatabaseLoader<T> : ScriptableObject where T: IDatabaseObject
    {
        [SerializeField]
        string jsonFilePath;
        protected string JSONFilePath { get { return "GameData/"+jsonFilePath + "/"; } }

        protected int id = 0;

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
