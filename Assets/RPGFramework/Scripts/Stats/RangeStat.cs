using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class RangeStat : ModifiableStat
    {
        public RangeStat(StatType newStatType, int initialMaxValue) : base (newStatType, initialMaxValue)
        {
            Type = newStatType;
            Max = initialMaxValue;
            Value = initialMaxValue;
        }

        [SerializeField]
        protected int max;
        public Action<int, int> MaxChanged { get; set; }

        public override int BaseValue
        {
            get { return max; }
            set
            {
                Max = value;
            }
        }

        public override int Value
        {
            get { return baseValue; }
            set
            {
                int old = baseValue;

                if (value < 0)
                    baseValue = 0;
                else if (value > Max)
                    baseValue = Max;
                else
                    baseValue = value;
                    

                if (ValueChanged != null)
                    ValueChanged(old, baseValue);
            }
        }

        public override int ModifiedValue
        {
            get { return modifiedValue; }
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

        public virtual int Max
        {
            get { return max; }
            set
            {
                int old = max;

                if (value > 0)
                    max = value;
                else
                    max = 0;

                if (Value < max)
                    Value = max;

                if (MaxChanged != null)
                    MaxChanged(old, modifiedValue);
            }
        }


    }
}
