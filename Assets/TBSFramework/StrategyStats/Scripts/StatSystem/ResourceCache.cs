using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ResourceCache 
    {
        [SerializeField]
        protected StatType type;
        public StatType Type
        {
            get { return type; }
            protected set
            {
                type = value;
            }
        }
        [SerializeField]
        protected bool accumulate = true;
        public bool Accumulate
        {
            get { return accumulate; }
            protected set
            {
                accumulate = value;
            }
        }
        [SerializeField]
        protected int amount = 0;
        public Action<int> AmountChanged;
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = MathTools.Clamp(value, 0, Max);

                if (AmountChanged != null)
                    AmountChanged(amount);
            }
        }
        [SerializeField]
        protected int max = 0;
        public Action<int> MaxChanged;
        public int Max
        {
            get { return max; }
            set
            {
                max = MathTools.ClampPositive(value);

                if (value < Amount)
                    Amount = value;

                if (MaxChanged != null)
                    MaxChanged(max);
            }
        }
        [SerializeField]
        protected int rate = 0;
        public Action<int> RateChanged;
        public int Rate
        {
            get { return rate; }
            set
            {
                rate = value;

                if (RateChanged != null)
                    RateChanged(rate);
            }
        }

        public ResourceCache (StatType type, int initialAmt, int initialMax, bool accumulate)
        {
            Type = type;
            Max = initialMax;
            Amount = initialAmt;
            Accumulate = accumulate;
            TurnManager.Instance.TurnChanged += OnTurnChange;
        }

        public void ModifyAmount (Stat stat)
        {
            Amount += stat;

            if (stat.Type != Type)
                Debug.LogWarning("Stat of type " + stat.Type + " modified resource cache of type " + Type);
        }

        public void ModifyRate (Stat stat)
        {
            Rate += stat;

            if (stat.Type != Type)
                Debug.LogWarning("Stat of type " + stat.Type + " modified resource cache of type " + Type);
        }

        void OnTurnChange (BaseTurn newTurn)
        {
            if (!Accumulate)
                Amount = 0;


            Amount += Rate;
            Rate = 0;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TurnManager.Instance.TurnChanged -= OnTurnChange;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
