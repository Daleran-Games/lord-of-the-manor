using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class NullStatCollection : IStatCollection<StatType>
    {
        public List<StatType> Types
        {
            get
            {
                return new List<StatType>(0);
            }
        }

        public event Action<IStatCollection<StatType>, StatType> StatModified;

        public int this[StatType statType]
        {
            get
            {
                return 0;
            }
        }

        public void Add(Modifier mod)
        {

        }

        public void Add(IList<Modifier> mods)
        {

        }

        public void Clear(StatType statType)
        {

        }

        public void ClearAll()
        {

        }

        public void Remove(Modifier mod)
        {
            
        }

        public void Remove(IList<Modifier> mods)
        {

        }

        public List<Modifier> GetAllOfType(StatType statType)
        {
            return new List<Modifier>(0);
        }

        public List<Modifier> GetAll()
        {
            return new List<Modifier>(0);
        }

        public bool Contains(StatType statType)
        {
            return false;
        }
    }
}

