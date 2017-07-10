using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierCollection
    {
        protected Dictionary<Stat.Category, List<Modifier>> modifiers;
        protected Dictionary<Stat.Category, int> totals;

        public ModifierCollection()
        {
            modifiers = new Dictionary<Stat.Category, List<Modifier>>();
            totals = new Dictionary<Stat.Category, int>();
        }

        public int this[Stat.Category statCat]
        {
            get
            {
                if (totals.ContainsKey(statCat))
                    return totals[statCat];
                else
                    return 0;
            }
        }

        public Modifier[] GetAll(Stat.Category statCat)
        {
            if (modifiers.ContainsKey(statCat))
                return modifiers[statCat].ToArray();
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

        public void Clear (Stat.Category statCat)
        {
            if (totals.ContainsKey(statCat))
                totals[statCat] = 0;

            if (modifiers.ContainsKey(statCat))
                modifiers[statCat].Clear();
        }

        public void ClearAll()
        {
            modifiers.Clear();
            totals.Clear();
        }
    }
}
