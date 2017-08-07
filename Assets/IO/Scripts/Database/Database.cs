using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.IO
{
    public class Database<T> where T : IDatabaseObject
    {

        Dictionary<string, int> keys;
        Dictionary<int, T> dict;
        public Dictionary<int, T> Dict { get { return dict; } }

        public int Count { get { return dict.Count; } }

        public Database()
        {
            keys = new Dictionary<string, int>();
            dict = new Dictionary<int, T>();
        }

        public void Add(T obj) 
        {
            keys.Add(obj.Name, obj.ID);
            dict.Add(obj.ID, obj);
        }

        public T this[int id] 
        {
            get
            {
                T obj;
                if (dict.TryGetValue(id, out obj))
                    return obj;

                throw new KeyNotFoundException(id + " not found in " + typeof(T).ToString() + "database.");
            }

        }

                
        public T this[string name]
        {
            get
            {
                T obj;
                if (dict.TryGetValue(LookupID(name), out obj))
                    return obj;

                throw new KeyNotFoundException(name + " not found in " + typeof(T).ToString() + "database.");
            }

        }

        public int LookupID (string name)
        {
            int id = 0;
            if (keys.TryGetValue(name, out id))
                return id;

            throw new KeyNotFoundException(name + " key not found " + typeof(T).ToString() + "database.");
        }

        public bool Contains(int id)
        {
            return dict.ContainsKey(id);
        }

        public bool Contains(string name)
        {
            return keys.ContainsKey(name);
        }

        public void Clear()
        {
            dict.Clear();
            keys.Clear();
            Debug.LogWarning(typeof(T).ToString() + " database cleared. ");
        }

    }
}
