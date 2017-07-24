using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class NullStatCollection : IStatCollection<StatType>
    {
        public StatType[] Types
        {
            get
            {
                return new StatType[0];
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

        public void Add(Modifier[] mods)
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

        public void Remove(Modifier[] mods)
        {

        }

        public Modifier[] GetAllOfType(StatType statType)
        {
            return new Modifier[0];
        }

        public Modifier[] GetAll()
        {
            return new Modifier[0];
        }

        public bool Contains(StatType statType)
        {
            return false;
        }
    }
}

