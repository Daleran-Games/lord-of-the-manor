using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Game;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Clan 
    {
        public string Name = "ClanName";
        public float GrowthRate = 0.025f;

        public int FoodPerPop = 1;
        int lastRate = 0;

        public int WoodPerPopInWinter = -1;
        public int WorkerPerPop = 6;

        public ResourceCache Population = new ResourceCache(StatType.Population, 6, 6, true);
        public ResourceCache Food = new ResourceCache(StatType.Food, 50, 150, true);
        public ResourceCache Wood = new ResourceCache(StatType.Wood, 30, 100, true);
        public ResourceCache Stone = new ResourceCache(StatType.Stone, 50, 150, true);
        public ResourceCache Coins = new ResourceCache(StatType.Coins, 100, 9999999, true);
        public ResourceCache Work = new ResourceCache(StatType.Work, 0, 99999999, false);

        public Clan(string name)
        {
            Name = name;
            GameManager.Instance.Play.StateEnabled += OnGameStart;
        }


        void OnGameStart(GameState state)
        {
            TurnManager.Instance.TurnChanged += OnTurnChange;
        }

        void OnTurnChange(BaseTurn newTurn)
        {
            for (int i=0; i < Population.Amount;i++)
            {
                if (Random.Bool(GrowthRate))
                    Population.Rate += 1;
            }

            if (newTurn is FallTurn)
                Wood.Rate += WoodPerPopInWinter * Population.Amount;

            int FoodUseNextTurn = Population.Amount * FoodPerPop;
            Food.Rate += FoodUseNextTurn;

            if (Food.Amount  < FoodUseNextTurn + Food.Rate)
                Population.Rate -= FoodUseNextTurn - Food.Rate;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GameManager.Instance.Play.StateEnabled -= OnGameStart;
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