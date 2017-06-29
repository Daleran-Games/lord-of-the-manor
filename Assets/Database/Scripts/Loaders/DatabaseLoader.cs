using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Database
{
    public abstract class DatabaseLoader<T> : MonoBehaviour where T: IDatabaseObject
    {
        [SerializeField]
        string filePath;
        protected string FilePath { get { return Application.dataPath + filePath; } }

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
