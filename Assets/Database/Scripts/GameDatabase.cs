using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;
using DaleranGames.TBSFramework;

namespace DaleranGames.Database
{
    public class GameDatabase : Singleton<GameDatabase>
    {

        protected GameDatabase() { }

        [SerializeField]
        ScriptableObject[] inputObjects;

        Dictionary<string, ScriptableObject> stringDict;
        Dictionary<int, ScriptableObject> intDict;

        public System.Action DatabaseInitialized;


        public void InitializeDatabase()
        {
            stringDict = new Dictionary<string, ScriptableObject>();
            intDict = new Dictionary<int, ScriptableObject>();
            for (int i=0; i < inputObjects.Length; i++)
            {
                stringDict.Add(inputObjects[i].name, inputObjects[i]);
                intDict.Add(inputObjects[i].GetInstanceID(), inputObjects[i]);
            }

        }

        public T GetDatabaseObject<T> (string name) where T : ScriptableObject
        {
            ScriptableObject fetchedObject;
            if(stringDict.TryGetValue(name, out fetchedObject))
            {
                T obj = fetchedObject as T;
                if (obj != null)
                {
                    return obj;
                }
            }
            //Debug.LogError("Database Error: " + name + " requested but not found.");
            return null;
        }

        public T GetDatabaseObject<T>(int id) where T : ScriptableObject
        {
            ScriptableObject fetchedObject;
            if (intDict.TryGetValue(id, out fetchedObject))
            {
                T obj = fetchedObject as T;
                if (obj != null)
                {
                    return obj;
                }
            }
            //Debug.LogError("Database Error: " + name + " requested but not found.");
            return null;
        }

        public int LookupID (string name)
        {
            ScriptableObject fetchedObject;
            if (stringDict.TryGetValue(name, out fetchedObject))
            {
                return fetchedObject.GetInstanceID();
            }
            Debug.LogError("Database Error: " + name + " is not in database and does not have an id.");
            return 0;
        }

    }
}

