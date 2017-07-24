using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierDatabase<T>
    {
        protected Dictionary<T, StatCollection> modifiers;
        

        public ModifierDatabase ()
        {
            modifiers = new Dictionary<T, StatCollection>();
        }

        public virtual StatCollection this[T obj]
        {
            get
            {
                if (modifiers.ContainsKey(obj))
                {
                    return modifiers[obj];
                }
                return null;
            }
        }

        public void Add(T obj, Modifier mod)
        {
            if (!modifiers.ContainsKey(obj))
            {
                modifiers.Add(obj, new StatCollection());
            }
            modifiers[obj].Add(mod);
        }

        public void Remove(T obj, Modifier mod)
        {
            if (modifiers.ContainsKey(obj))
            {
                modifiers[obj].Remove(mod);
            }
        }

        public bool Contains(T obj)
        {
            return modifiers.ContainsKey(obj);
        }

        public void ClearAll()
        {
            modifiers.Clear();
        }
    }
}
