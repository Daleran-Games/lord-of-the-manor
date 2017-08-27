using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DaleranGames.UI;


namespace DaleranGames.TBSFramework
{
    public abstract class GoodsCollection : IGoodsCollection
    {

        [System.NonSerialized]
        protected List<Transaction> currentTransactions;
        public virtual List<Transaction> CurrentTransactions { get { return currentTransactions; } }

        [System.NonSerialized]
        protected List<Transaction> pendingTransactions;
        public virtual List<Transaction> PendingTransactions { get { return pendingTransactions; } }

        public event Action<GoodsCollection, GoodType> FutureTransactionsChanged;
        public event Action<GoodsCollection, GoodType> GoodChanged;

        public abstract int this[GoodType type] { get; protected set; }
        public abstract List<Good> Goods { get; }

        public GoodsCollection()
        {
            currentTransactions = new List<Transaction>();
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

        public virtual void ProcessNow(Transaction transaction)
        {
            if(transaction.Value !=0)
            {
                this[transaction.Type] += transaction.Value;
                currentTransactions.Add(transaction);
            }
                
        }

        public virtual void ProcessNow(IList<Transaction> transactions)
        {
                for (int i=0;i<transactions.Count;i++)
                {
                    ProcessNow(transactions[i]);
                }
        }

        private void Add (Transaction transaction)
        {
            if (transaction.Value != 0)
            {
                pendingTransactions.Add(transaction);
                OnPendingTransactionsChanged(this, transaction.Type);
            }
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
            /*
            for (int i=0;i<pendingTransactions.Count;i++)
            {
                if (pendingTransactions[i].Type == transaction.Type && pendingTransactions[i].Value == transaction.Value && pendingTransactions[i].Description == transaction.Description)
                {
                    pendingTransactions.RemoveAt(i);
                    OnPendingTransactionsChanged(this, transaction.Type);
                    Debug.Log("It fired!");
                    return;
                }
            }
            */
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

        public virtual Good GetTotalPendingTransactionsOfType (GoodType type)
        {
            int total = 0;
            for (int i=0;i<pendingTransactions.Count;i++)
            {
                if (pendingTransactions[i].Type == type)
                    total += pendingTransactions[i].Value;
            }
            return new Good(type, total);
        }

        public virtual List<Transaction> GetAllPendingTransactionsOfType (GoodType type, bool consolidateDescriptions)
        {
            List<Transaction> results = new List<Transaction>();
            for (int i = 0; i < pendingTransactions.Count; i++)
            {
                if (pendingTransactions[i].Type == type)
                {
                    if (consolidateDescriptions)
                    {
                        CheckAndConsolidateDescriptions(pendingTransactions[i], results);
                    }else
                        results.Add(pendingTransactions[i]);
                }
            }
            return results;
        }

        public virtual List<Transaction> GetAllCurrentTransactionsOfType(GoodType type, bool consolidateDescriptions)
        {
            List<Transaction> results = new List<Transaction>();
            for (int i = 0; i < currentTransactions.Count; i++)
            {
                if (currentTransactions[i].Type == type)
                {
                    if (consolidateDescriptions)
                    {
                        CheckAndConsolidateDescriptions(currentTransactions[i], results);
                    }
                    else
                        results.Add(currentTransactions[i]);
                }
            }
            return results;
        }

        void CheckAndConsolidateDescriptions(Transaction transaction, List<Transaction> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == transaction.Type )
                {
                    if (list[i].Description == transaction.Description && (transaction.Value + list[i].Value) !=0)
                    {
                        list[i] = new Transaction(transaction.Type, transaction.Value + list[i].Value, transaction.Description);
                        return;
                    } 
                }
            }
            list.Add(transaction);
        }

        public virtual void ProcessAllPendingTransactions()
        {
            for (int i=0;i < pendingTransactions.Count; i++)
            {
                this[pendingTransactions[i].Type] += pendingTransactions[i].Value;
            }
            currentTransactions.Clear();
            currentTransactions.AddRange(pendingTransactions);
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

