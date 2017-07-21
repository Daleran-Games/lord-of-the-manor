using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierDatabase<T>
    {
        protected Dictionary<T, ModifierCollection> modifiers;
        

        public ModifierDatabase ()
        {
            modifiers = new Dictionary<T, ModifierCollection>();
        }

        public virtual ModifierCollection this[T obj]
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
                modifiers.Add(obj, new ModifierCollection());
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
