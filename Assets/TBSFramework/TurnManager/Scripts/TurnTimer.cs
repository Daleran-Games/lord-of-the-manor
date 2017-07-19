using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TurnTimer : System.IDisposable
    {
        protected TurnManager turnManager;

        public TurnTimer(TurnManager turnManager)
        {
            this.turnManager = turnManager;
            this.turnManager.TurnEnded += OnTurnEnd;
        }

        public event System.Action TimerExpired;

        bool running = false;
        public bool Running
        {
            get
            {
                return running;
            }
        }

        int turns = 0;
        public int Turns
        {
            get { return turns; }
            set
            {
                if (value > 0)
                {
                    turns = value;
                    running = true;
                }
                    
            }
        }

        void OnTurnEnd (BaseTurn turn)
        {
            if (Running)
            {
                if (turns > 0)
                    turns--;

                if (turns == 0)
                {
                    if (TimerExpired != null)
                        TimerExpired();

                    running = false;
                }
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


    }
}

