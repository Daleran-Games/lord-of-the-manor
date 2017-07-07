using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Unit
    {
        protected string name;
        public string Name { get { return name; } }

        public Unit (string name, UnitType type)
        {
            this.name = name;
            UnitType = type;

            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
            GameManager.Instance.StateChanged += OnGameStart;
        }

#region UnitType

        [SerializeField]
        protected UnitType unitType;
        public virtual UnitType UnitType
        {
            get { return unitType; }
            set
            {
                if (unitType != null)
                    unitType.OnDeactivation(this);

                unitType = value;

                if (unitType != null)
                    unitType.OnActivation(this);
            }
        }

        void OnTurnEnd(BaseTurn turn)
        {
            if (UnitType != null)
                UnitType.OnTurnEnd(turn, this);
        }

        void OnTurnSetUp(BaseTurn turn)
        {
            if (UnitType != null)
                UnitType.OnTurnSetUp(turn, this);
        }

        void OnTurnStart(BaseTurn turn)
        {
            if (UnitType != null)
                UnitType.OnTurnStart(turn, this);
        }

        void OnGameStart(GameState state)
        {
            if (UnitType != null)
                UnitType.OnGameStart(this);
        }
#endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TurnManager.Instance.TurnEnded -= OnTurnEnd;
                    TurnManager.Instance.TurnSetUp -= OnTurnSetUp;
                    TurnManager.Instance.TurnStart += OnTurnStart;
                    GameManager.Instance.StateChanged -= OnGameStart;
                    UnitType.OnDeactivation(this);
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
