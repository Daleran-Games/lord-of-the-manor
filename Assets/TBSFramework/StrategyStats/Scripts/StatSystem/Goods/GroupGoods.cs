using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class GroupGoods : IGoodsCollection
    {
        protected Group owner;

        protected int food;
        public virtual Good Food { get { return new Good(GoodType.Food, food); } }

        protected int wood;
        public virtual Good Wood { get { return new Good(GoodType.Wood, wood); } }

        protected int stone;
        public virtual Good Stone { get { return new Good(GoodType.Stone, stone); } }

        protected int gold;
        public virtual Good Gold { get { return new Good(GoodType.Gold, gold); } }

        protected int population;
        public virtual Good Population { get { return new Good(GoodType.Population, population); } }

        protected int work;
        public virtual Good Work { get { return new Good(GoodType.Work, work); } }



        protected List<Transaction> endTurnTransactions;
        public virtual Transaction[] PendingTransactions { get { return endTurnTransactions.ToArray(); } }

        protected Action<Good> goodDepleted;
        public Action<Good> GoodDepleted  {  get { return goodDepleted; } }

        public GroupGoods (Group unit)
        {
            endTurnTransactions = new List<Transaction>();
            owner = unit;
        }

        public virtual Good this[GoodType type]
        {
            get
            {
                switch(type)
                {
                    case GoodType.Food:
                        return Food;
                    case GoodType.Wood:
                        return Wood;
                    case GoodType.Stone:
                        return Stone;
                    case GoodType.Gold:
                        return Gold;
                    case GoodType.Population:
                        return Population;
                    case GoodType.Work:
                        return Work;
                    default:
                        return null;
                }
            }
        }

        public virtual Good[] GetAllGoods()
        {
            return new Good[]
            {
                Food,
                Wood,
                Stone,
                Gold,
                Population,
                Work
            };
        }

        public virtual bool CanProcessTransaction (Transaction transaction)
        {
            if (ContainsGoodOfType(transaction.TransactedGood))
            {
                if (transaction.Immediate == true && this[transaction.TransactedGood.Type].Value >= transaction.TransactedGood.Value)
                    return true;
                else if (transaction.Immediate == false)
                    return true;
            }
            return false;
        }

        public virtual bool CanProcessTransaction(Transaction[] transactions)
        {
            for (int i=0;i<transactions.Length;i++)
            {
                if (!CanProcessTransaction(transactions[i]))
                    return false;
            }
            return true;
        }

        public virtual bool Add(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public virtual bool Add(Transaction[] transactions)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove (Transaction transaction)
        {

        }

        public virtual void Remove(Transaction[] transactions)
        {

        }

        public virtual void Set(Transaction transaction)
        {

        }

        public virtual bool ContainsGoodOfType (GoodType type)
        {
            if (
                type == GoodType.Food ||
                type == GoodType.Wood ||
                type == GoodType.Stone ||
                type == GoodType.Gold ||
                type == GoodType.Work ||
                type == GoodType.Population 
                )
                return true;
            else
                return false;
        }

        public virtual void ProcessTransactions()
        {

        }

        public virtual void ResolveEdgeCases()
        {

        }




    }
}
