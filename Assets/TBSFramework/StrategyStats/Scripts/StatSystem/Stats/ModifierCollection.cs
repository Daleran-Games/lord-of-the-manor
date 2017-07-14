using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierCollection : IModifierCollection
    {
        protected Dictionary<StatType, List<Modifier>> modifiers;
        protected Dictionary<StatType, int> totals;

        public ModifierCollection()
        {
            modifiers = new Dictionary<StatType, List<Modifier>>();
            totals = new Dictionary<StatType, int>();
        }

        public int this[StatType statType]
        {
            get
            {
                if (totals.ContainsKey(statType))
                    return totals[statType];
                else
                    return 0;
            }
        }

        public Modifier[] GetAll(StatType statType)
        {
            if (modifiers.ContainsKey(statType))
                return modifiers[statType].ToArray();
            else
                return null;
        }

        public void Add (Modifier mod)
        {
            if (mod.Mod.Value != 0)
            {
                if (!modifiers.ContainsKey(mod.Mod.Type))
                {
                    modifiers.Add(mod.Mod.Type, new List<Modifier>());
                    totals.Add(mod.Mod.Type, 0);
                }
                modifiers[mod.Mod.Type].Add(mod);
                totals[mod.Mod.Type] += mod.Mod.Value;
            }
        }
        
        public void Add (ModifierEntry[] mods)
        {
            for (int i=0; i < mods.Length; i++)
            {
                Add(mods[i].GetModifer());
            }
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

        public void Remove(ModifierEntry[] mods)
        {
            for (int i = 0; i < mods.Length; i++)
            {
                Remove(mods[i].GetModifer());
            }
        }

        public void Clear (StatType statType)
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
