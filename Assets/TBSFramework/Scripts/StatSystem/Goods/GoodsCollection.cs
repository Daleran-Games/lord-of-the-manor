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

        public event Action<GoodsCollection, GoodType> FutureTransactionsChanged;
        public event Action<GoodsCollection, GoodType> GoodChanged;

        public abstract int this[GoodType type] { get; set; }
        public abstract List<Good> Goods { get; }

        public GoodsCollection()
        {
            pendingTransactions = new List<Transaction>();
        }

        public virtual bool CanProcessNow(Transaction transaction)
        {

            if (ContainsGoodOfType(transaction.Type) && (this[transaction.Type] + transaction.Value) >= 0)
            {
                return true;
            } else
                return false;
        }

        public virtual bool CanProcessNow(IList<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                if (!CanProcessNow(transactions[i]))
                    return false;
            }
            return true;
        }

        protected virtual void Process(Transaction transaction)
        {
            this[transaction.Type] += transaction.Value;
        }

        public virtual bool TryProcessNow(Transaction transaction)
        {
            if (CanProcessNow(transaction))
            {
                Process(transaction);

                return true;
            }
            return false;
        }

        public virtual bool TryProcessNow(IList<Transaction> transactions)
        {
            if (CanProcessNow(transactions))
            {
                for (int i=0;i<transactions.Count;i++)
                {
                    Process(transactions[i]);
                }
                return true;
            }
            return false;
        }

        private void Add (Transaction transaction)
        {
            pendingTransactions.Add(transaction);
            OnPendingTransactionsChanged(this, transaction.Type);
        }

        public virtual void AddFuture(Transaction transaction)
        {
            Add(transaction);
        }

        public virtual void AddFuture(IList<Transaction> transactions)
        {
            for (int i = 0; i < transactions.Count; i++)
            {
                Add(transactions[i]);
            }
        }

        private void Remove(Transaction transaction)
        {
            pendingTransactions.Remove(transaction);
            OnPendingTransactionsChanged(this, transaction.Type);
        }

        public virtual void RemoveFuture(Transaction transaction)
        {
            Remove(transaction);
        }

        public virtual void RemoveFuture(IList<Transaction> transactions)
        {
            for (int i=0; i < transactions.Count; i++)
            {
                Remove(transactions[i]);
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

        public virtual void ProcessAllPendingTransactions()
        {
            for (int i=0;i < pendingTransactions.Count; i++)
            {
                this[pendingTransactions[i].Type] += pendingTransactions[i].Value;
            }
            pendingTransactions.Clear();
            OnAllPendingTransactionsChanged(this);

        }

        public abstract void ResolveEdgeCases();

        protected virtual void OnPendingTransactionsChanged(GoodsCollection col, GoodType type)
        {
            if (FutureTransactionsChanged != null)
                FutureTransactionsChanged(col, type);
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

