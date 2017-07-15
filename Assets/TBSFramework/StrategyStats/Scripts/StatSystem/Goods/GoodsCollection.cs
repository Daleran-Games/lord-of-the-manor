using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    public abstract class GoodsCollection : IGoodsCollection
    {


        protected List<Transaction> pendingTransactions;
        public virtual Transaction[] PendingTransactions { get { return pendingTransactions.ToArray(); } }

        public event Action<Good> GoodDepleted;

        public abstract Good this[GoodType type] { get; set; }
        public abstract Good[] GetAllGoods();

        public GoodsCollection()
        {
            pendingTransactions = new List<Transaction>();
        }

        public virtual bool CanProcessTransaction(Transaction transaction)
        {
            if (ContainsGoodOfType(transaction.Good))
            {
                if (transaction.Immediate == true && this[transaction.Good.Type].Value >= transaction.Good.Value)
                    return true;
                else if (transaction.Immediate == false)
                    return true;
            }
            return false;
        }

        public virtual bool CanProcessTransaction(Transaction[] transactions)
        {
            for (int i = 0; i < transactions.Length; i++)
            {
                if (!CanProcessTransaction(transactions[i]))
                    return false;
            }
            return true;
        }

        public virtual bool TryAdd(Transaction transaction)
        {
            if (CanProcessTransaction(transaction))
            {
                Add(transaction);

                return true;
            }
            return false;
        }

        protected void Add(Transaction transaction)
        {
            if (transaction.Immediate)
                this[transaction.Good.Type] += transaction.Good.Value;
            else
                pendingTransactions.Add(transaction);
        }

        public virtual bool TryAdd(Transaction[] transactions)
        {
            if (CanProcessTransaction(transactions))
            {
                for (int i=0;i<transactions.Length;i++)
                {
                    Add(transactions[i]);
                }
                return true;
            }
            return false;
        }

        public virtual void Remove(Transaction transaction)
        {
            pendingTransactions.Remove(transaction);
        }

        public virtual void Remove(Transaction[] transactions)
        {
            for (int i=0; i < transactions.Length; i++)
            {
                pendingTransactions.Remove(transactions[i]);
            }
        }

        public abstract bool ContainsGoodOfType(GoodType type);

        public virtual void ProcessTransactions()
        {
            for (int i=0;i < pendingTransactions.Count; i++)
            {
                this[pendingTransactions[i]] += pendingTransactions[i];
            }
            pendingTransactions.Clear();
        }

        public abstract void ResolveEdgeCases();
    }
}

