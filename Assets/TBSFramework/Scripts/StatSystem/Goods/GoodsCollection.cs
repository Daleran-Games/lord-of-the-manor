using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    public abstract class GoodsCollection : IGoodsCollection
    {

        [SerializeField]
        protected List<Transaction> pendingTransactions;
        public virtual Transaction[] PendingTransactions { get { return pendingTransactions.ToArray(); } }

        public event Action<GoodsCollection, GoodType> PendingTransactionsChanged;
        public event Action<GoodsCollection, GoodType> GoodChanged;

        public abstract int this[GoodType type] { get; set; }
        public abstract Good[] Goods { get; }

        public GoodsCollection()
        {
            pendingTransactions = new List<Transaction>();
        }

        public virtual bool CanProcessTransaction(Transaction transaction)
        {
            if (ContainsGoodOfType(transaction.Good))
            {
                if (transaction.Immediate == true && this[transaction.Good.Type] >= transaction.Good.Value)
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


        protected void Add(Transaction transaction)
        {
            if (transaction.Immediate)
            {
                this[transaction.Good.Type] += transaction.Good.Value;
            }
            else
            {
                pendingTransactions.Add(transaction);
                OnPendingTransactionsChanged(this, transaction.Good.Type);
            }

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
            OnPendingTransactionsChanged(this, transaction.Good.Type);
        }

        public virtual void Remove(Transaction[] transactions)
        {
            for (int i=0; i < transactions.Length; i++)
            {
                pendingTransactions.Remove(transactions[i]);
                OnPendingTransactionsChanged(this, transactions[i].Good.Type);
            }
        }

        public abstract bool ContainsGoodOfType(GoodType type);

        public virtual Good GetAllPendingTransactionsOfType (GoodType type)
        {
            int total = 0;
            for (int i=0;i<pendingTransactions.Count;i++)
            {
                if (pendingTransactions[i].Good.Type == type)
                    total += pendingTransactions[i].Good.Value;
            }
            return new Good(type, total);
        }

        public virtual void ProcessAllTransactions()
        {
            for (int i=0;i < pendingTransactions.Count; i++)
            {
                this[pendingTransactions[i]] += pendingTransactions[i];
            }
            pendingTransactions.Clear();
            OnAllPendingTransactionsChanged(this);

        }

        public abstract void ResolveEdgeCases();

        protected virtual void OnPendingTransactionsChanged(GoodsCollection col, GoodType type)
        {
            if (PendingTransactionsChanged != null)
                PendingTransactionsChanged(col, type);
        }

        protected virtual void OnAllPendingTransactionsChanged(GoodsCollection col)
        {
            for (int i=0;i< Goods.Length;i++)
            {
                OnPendingTransactionsChanged(this, Goods[i].Type);
            }
        }

        protected virtual void OnGoodChanged(GoodsCollection col, GoodType type)
        {
            if (GoodChanged != null)
                GoodChanged(col, type);
        }
    }
}

