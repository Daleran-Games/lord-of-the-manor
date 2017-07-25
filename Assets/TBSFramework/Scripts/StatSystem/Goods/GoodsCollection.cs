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
        public virtual List<Transaction> PendingTransactions { get { return pendingTransactions; } }

        public event Action<GoodsCollection, GoodType> PendingTransactionsChanged;
        public event Action<GoodsCollection, GoodType> GoodChanged;

        public abstract int this[GoodType type] { get; set; }
        public abstract List<Good> Goods { get; }

        public GoodsCollection()
        {
            pendingTransactions = new List<Transaction>();
        }

        public virtual bool CanProcessTransaction(Transaction transaction)
        {
            if (ContainsGoodOfType(transaction.Type))
            {
                if (transaction.Immediate == true && this[transaction.Type] >= transaction.Value)
                    return true;
                else if (transaction.Immediate == false)
                    return true;
            }
            return false;
        }

        public virtual bool CanProcessTransaction(IList<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                if (!CanProcessTransaction(transactions[i]))
                    return false;
            }
            return true;
        }

        public abstract bool CanProcessCost(Cost cost);

        public virtual bool CanProcessCost(IList<Cost> costs)
        {
            for (int i = 0; i < costs.Count; i++)
            {
                if (!CanProcessCost(costs[i]))
                    return false;
            }
            return true;
        }

        protected void Add(Transaction transaction)
        {
            if (transaction.Immediate)
            {
                this[transaction.Type] += transaction.Value;
            }
            else
            {
                pendingTransactions.Add(transaction);
                OnPendingTransactionsChanged(this, transaction.Type);
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

        public virtual bool TryAdd(IList<Transaction> transactions)
        {
            if (CanProcessTransaction(transactions))
            {
                for (int i=0;i<transactions.Count;i++)
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
            OnPendingTransactionsChanged(this, transaction.Type);
        }

        public virtual void Remove(IList<Transaction> transactions)
        {
            for (int i=0; i < transactions.Count; i++)
            {
                pendingTransactions.Remove(transactions[i]);
                OnPendingTransactionsChanged(this, transactions[i].Type);
            }
        }

        public abstract bool ContainsGoodOfType(GoodType type);

        public virtual Good GetAllPendingTransactionsOfType (GoodType type)
        {
            int total = 0;
            for (int i=0;i<pendingTransactions.Count;i++)
            {
                if (pendingTransactions[i].Type == type)
                    total += pendingTransactions[i].Value;
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
            for (int i=0;i< Goods.Count;i++)
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

