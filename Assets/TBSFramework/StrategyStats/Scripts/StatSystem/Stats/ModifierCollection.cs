using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierCollection<T>
    {
        protected Dictionary<T, ModifierDictionary> modifiers;
        

        public ModifierCollection ()
        {
            modifiers = new Dictionary<T, ModifierDictionary>();
        }

        public virtual ModifierDictionary this[T obj]
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
                modifiers.Add(obj, new ModifierDictionary());
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

        public void Clear()
        {
            modifiers.Clear();
        }
    }
}
