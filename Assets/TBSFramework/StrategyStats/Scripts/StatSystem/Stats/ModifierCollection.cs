using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierCollection
    {
        protected Dictionary<string, List<Modifier>> modifiers;
        protected Dictionary<string, int> totals;

        public ModifierCollection()
        {
            modifiers = new Dictionary<string, List<Modifier>>();
            totals = new Dictionary<string, int>();
        }

        public int this[string statType]
        {
            get
            {
                if (totals.ContainsKey(statType))
                    return totals[statType];
                else
                    return 0;
            }
        }

        public Modifier[] GetAll(string statType)
        {
            if (modifiers.ContainsKey(statType))
                return modifiers[statType].ToArray();
            else
                return null;
        }

        public void Add (Modifier mod)
        {
            if (!modifiers.ContainsKey(mod.Mod.Type))
            {
                modifiers.Add(mod.Mod.Type, new List<Modifier>());
                totals.Add(mod.Mod.Type, 0);
            }
            modifiers[mod.Mod.Type].Add(mod);
            totals[mod.Mod.Type] += mod.Mod.Value;
        }

        public void Remove (Modifier mod)
        {
            if (modifiers.ContainsKey(mod.Mod.Type))
            {
                if (modifiers[mod.Mod.Type].Contains(mod))
                {
                    modifiers[mod.Mod.Type].Remove(mod);
                    totals[mod.Mod.Type] -= mod.Mod.Value;
                }
            }
        }

        public void Clear (string statType)
        {
            if (totals.ContainsKey(statType))
                totals[statType] = 0;

            if (modifiers.ContainsKey(statType))
                modifiers[statType].Clear();
        }

        public void ClearAll()
        {
            modifiers.Clear();
            totals.Clear();
        }
    }
}
