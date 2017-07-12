using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UnitGoods
    {


        protected int food;
        public Good Food { get { return new Good(Good.Category.Food, food); } }

        protected int wood;


        protected int stone;


        protected int gold;


        protected int population;


        protected int work;



        protected List<Transaction> endTurnTransactions;

        public UnitGoods (Unit unit)
        {
            endTurnTransactions = new List<Transaction>();
        }

        public bool CanProcessTransaction (Transaction transaction)
        {
            //if (ContainsGood(transaction.TransactedGood))


            return true;
        }

        public void Add(Transaction transaction)
        {

        }

        public void Remove (Transaction transaction)
        {

        }

        public void Set(Transaction transaction)
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
