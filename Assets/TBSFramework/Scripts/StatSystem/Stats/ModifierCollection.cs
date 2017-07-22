﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierCollection : IModifierCollection
    {
        protected Dictionary<StatType, List<Modifier>> modifiers;
        protected Dictionary<StatType, int> totals;

        public event Action<IModifierCollection, StatType> StatModified;

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
            if (mod.Stat.Value != 0)
            {
                if (!modifiers.ContainsKey(mod.Stat.Type))
                {
                    modifiers.Add(mod.Stat.Type, new List<Modifier>());
                    totals.Add(mod.Stat.Type, 0);
                }
                modifiers[mod.Stat.Type].Add(mod);
                totals[mod.Stat.Type] += mod.Stat.Value;

                if (StatModified != null)
                    StatModified(this, mod.Stat.Type);
            }
        }
        
        public void Add (Modifier[] mods)
        {
            for (int i=0; i < mods.Length; i++)
            {
                Add(mods[i]);
            }
        }

        public void Remove (Modifier mod)
        {
            if (modifiers.ContainsKey(mod.Stat.Type))
            {
                if (modifiers[mod.Stat.Type].Contains(mod))
                {
                    modifiers[mod.Stat.Type].Remove(mod);
                    totals[mod.Stat.Type] -= mod.Stat.Value;

                    if (StatModified != null)
                        StatModified(this, mod.Stat.Type);
                }
            }
        }

        public void Remove(Modifier[] mods)
        {
            for (int i = 0; i < mods.Length; i++)
            {
                Remove(mods[i]);
            }
        }

        public void Clear (StatType statType)
        {
            if (totals.ContainsKey(statType))
            {
                totals[statType] = 0;

                if (StatModified != null)
                    StatModified(this, statType);
            }
                

            if (modifiers.ContainsKey(statType))
            {
                modifiers[statType].Clear();

                if (StatModified != null)
                    StatModified(this, statType);
            }
                
        }

        public void ClearAll()
        {
            modifiers.Clear();
            totals.Clear();
        }
    }
}
