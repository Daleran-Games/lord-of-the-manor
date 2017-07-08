using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UnitGoods
    {

        protected UnitType unitType;

        protected int food;
        public Good Food { get { return new Good(Good.Category.Food, food); } }

        protected int wood;


        protected int stone;


        protected int gold;


        protected int population;


        protected int work;



        protected List<Transaction> nextTurnTransactions;

        public UnitGoods (UnitType type)
        {
            unitType = type;
            nextTurnTransactions = new List<Transaction>();
        }

        public bool CanProcessImmediateTransaction (Transaction transaction)
        {
            return true;
        }

        public bool ProcessImmediateTransaction(Transaction transaction)
        {
            return true;
        }

        public void Add(Transaction transaction)
        {

        }

        public void Remove (Transaction transaction)
        {

        }

        public bool ContainsGood (Good good)
        {
            if (
                good.Type == Good.Category.Food ||
                good.Type == Good.Category.Wood ||
                good.Type == Good.Category.Stone ||
                good.Type == Good.Category.Gold ||
                good.Type == Good.Category.Work ||
                good.Type == Good.Category.Population 
                )
                return true;
            else
                return false;
        }

        public void ProcessAllTransaction()
        {

        }

        public void ResolveEdgeCases()
        {

        }

    }
}
