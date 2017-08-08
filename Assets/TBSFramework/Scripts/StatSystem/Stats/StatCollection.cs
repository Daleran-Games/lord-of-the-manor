using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class StatCollection : IStatCollection<StatType>
    {
        [SerializeField]
        protected List<Modifier> debugModifiers;

        protected Dictionary<StatType, List<Modifier>> modifiers;
        protected Dictionary<StatType, int> totals;

        public event Action<IStatCollection<StatType>, StatType> StatModified;

        public static readonly NullStatCollection Null = new NullStatCollection();

        public StatCollection()
        {
            modifiers = new Dictionary<StatType, List<Modifier>>();
            totals = new Dictionary<StatType, int>();
            debugModifiers = new List<Modifier>();
        }

        public virtual int this[StatType statType]
        {
            get
            {
                if (totals.ContainsKey(statType))
                    return totals[statType];
                else
                    return 0;
            }
        }

        public virtual List<StatType> Types
        {
            get
            {
                return new List<StatType>(modifiers.Keys);
            }
        }

        public virtual List<Modifier> GetAllOfType(StatType statType)
        {
            if (modifiers.ContainsKey(statType))
                return new List<Modifier>(modifiers[statType]);
            else
                return null;
        }

        public virtual List<Modifier> GetAll()
        {
            List<Modifier> mods = new List<Modifier>();
            foreach(KeyValuePair<StatType,List<Modifier>> m in modifiers)
            {
                mods.AddRange(m.Value);
            }
            return mods;
        }

        public virtual bool Contains(StatType statType)
        {
            return modifiers.ContainsKey(statType);
        }

        public virtual void Add (Modifier mod)
        {
            if (mod.Value != 0)
            {
                if (!modifiers.ContainsKey(mod.Type))
                {
                    modifiers.Add(mod.Type, new List<Modifier>());
                    totals.Add(mod.Type, 0);
                }
                modifiers[mod.Type].Add(mod);
                totals[mod.Type] += mod.Value;
                debugModifiers.Add(mod);

                RaiseStatModified(this, mod.Type);
            }
        }
        
        public virtual void Add (IList<Modifier> mods)
        {
            for (int i=0; i < mods.Count; i++)
            {
                Add(mods[i]);
            }
        }

        public virtual void Remove (Modifier mod)
        {
            if (modifiers.ContainsKey(mod.Type))
            {
                if (modifiers[mod.Type].Contains(mod))
                {
                    modifiers[mod.Type].Remove(mod);
                    totals[mod.Type] -= mod.Value;
                    debugModifiers.Remove(mod);

                    RaiseStatModified(this, mod.Type);
                }
            }
        }

        public virtual void Remove(IList<Modifier> mods)
        {
            for (int i = 0; i < mods.Count; i++)
            {
                Remove(mods[i]);
            }
        }

        public virtual void Clear (StatType statType)
        {
            if (totals.ContainsKey(statType))
            {
                totals[statType] = 0;

                RaiseStatModified(this, statType);
            }
            if (modifiers.ContainsKey(statType))
            {
                modifiers[statType].Clear();

                RaiseStatModified(this, statType);
            }
        }

        public virtual void ClearAll()
        {
            modifiers.Clear();
            totals.Clear();
            debugModifiers.Clear();
        }

        protected virtual void RaiseStatModified(IStatCollection<StatType> stats, StatType statType)
        {
            if (StatModified != null)
                StatModified(stats, statType);
        }


    }
}
