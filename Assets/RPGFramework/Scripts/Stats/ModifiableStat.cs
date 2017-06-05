using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class ModifiableStat : Stat, IModifiable
    {
        public ModifiableStat (StatType newStatType, int initialBaseValue) : base (newStatType, initialBaseValue)
        {
            Type = newStatType;
            BaseValue = initialBaseValue;
        }

        [SerializeField]
        protected int modifiedValue;
        public Action<int, int> ModifiedValueChanged { get; set; }


        public virtual int BaseValue
        {
            get { return baseValue; }
            set
            {
                Value = value;
            }
        }

        public override int Value
        {
            get { return modifiedValue; }
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

        public virtual int ModifiedValue
        {
            get { return Value; }
            set
            {
                int old = modifiedValue;

                if (value > 0)
                    modifiedValue = value;
                else
                    modifiedValue = 0;

                if (ModifiedValueChanged != null)
                    ModifiedValueChanged(old, modifiedValue);
            }
        }

    }
}
