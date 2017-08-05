using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TurnCounters : System.IDisposable
    {
        protected TurnManager turnManager;

        [SerializeField]
        protected List<Counter> counters;


        public TurnCounters(TurnManager turnManager)
        {
            this.turnManager = turnManager;
            this.turnManager.TurnEnded += OnTurnEnd;
            counters = new List<Counter>();
        }

        public int this[StatType type]
        {
            get
            {
                for (int i = 0; i < counters.Count; i++)
                {
                    if (counters[i].Type == type)
                        return counters[i].Value;
                }
                return 0;
            }
        }

        public void AddCounter(StatType type)
        {
            if (!ContainsCounterOfType(type))
            {
                counters.Add(new Counter(type, 0));
            } else
            {
                Debug.LogWarning("Counter already exsists: " + type);
                return;
            }

        }

        public void PauseCounter(bool paused, StatType type)
        {
            for (int i = 0; i < counters.Count; i++)
            {
                if (counters[i].Type == type)
                {
                    counters[i].Paused = paused;
                }
            }
        }

        public void RemoveCounter (StatType type)
        {
            int index = 0;
            bool found = false;
            for (int i = 0; i < counters.Count; i++)
            {
                if (counters[i].Type == type)
                {
                    index = i;
                    found = true;
                }   
            }

            if (found)
                counters.Remove(counters[index]);
        }

        public bool ContainsCounterOfType (StatType type)
        {
            for (int i=0;i<counters.Count;i++)
            {
                if (counters[i].Type == type)
                    return true;
            }
            return false;
        }

        void OnTurnEnd (BaseTurn turn)
        {
            for (int i = 0; i < counters.Count; i++)
            {
                if (!counters[i].Paused)
                    counters[i].Value++;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    turnManager.TurnEnded -= OnTurnEnd;
                }

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
        [System.Serializable]
        protected class Counter
        {
            public StatType Type;
            public int Value;
            public bool Paused = false;

            public Counter(StatType type, int value)
            {
                Type = type;
                Value = value;
                Paused = false;
            }
            
        }

    }
}

