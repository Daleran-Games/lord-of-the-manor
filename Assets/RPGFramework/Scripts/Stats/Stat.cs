using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class Stat : IStat
    {

        public Stat(StatType newStatType, int initialValue)
        {
            Type = newStatType;
            Value = initialValue;
        }

        [SerializeField]
        protected StatType type;
        public StatType Type { get; protected set; }

        [SerializeField]
        protected int baseValue;
        public Action<int, int> ValueChanged { get; set; }

        public virtual int Value
        {
            get { return baseValue; }
            set
            {
                int old = baseValue;

                if (value > 0)
                    baseValue = value;
                else
                    baseValue = 0;

                if (ValueChanged != null)
                    ValueChanged(old, baseValue);
            }
        }


    }
}
