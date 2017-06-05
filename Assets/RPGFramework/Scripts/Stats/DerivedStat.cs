using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.RPGFramework
{
    public delegate int DerivedStatHandler(params IStat[] stats);

    [System.Serializable]
    public class DerivedStat : IStat, IDisposable
    {
        protected IStat[] stats;
        protected DerivedStatHandler statFunction;

        public DerivedStat(StatType newStatType, DerivedStatHandler derivedFunction, params IStat[] stats)
        {
            Type = newStatType;
            statFunction = derivedFunction;
            this.stats = stats;
            SubscribeToStats();
            UpdateStat(0, 0);
        }

        [SerializeField]
        protected StatType type;
        public StatType Type { get; protected set; }

        [SerializeField]
        protected int baseValue;
        public Action<int, int> ValueChanged { get; set; }
        public int Value
        {
            get { return baseValue; }
            protected set
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

        void UpdateStat(int i, int j)
        {
            if (statFunction != null && stats != null)
                Value = statFunction(stats);
            else
                Value = 0;
        }

        void SubscribeToStats ()
        {
            if (stats != null)
            {
                for (int i=0; i<stats.Length;i++)
                {
                    stats[i].ValueChanged += UpdateStat;
                }
            }
        }

        void UnsubscribeFromStats()
        {
            if (stats != null)
            {
                for (int i = 0; i < stats.Length; i++)
                {
                    stats[i].ValueChanged -= UpdateStat;
                }
            }
        }


        public void Dispose()
        {
            UnsubscribeFromStats();
        }
    }
}
