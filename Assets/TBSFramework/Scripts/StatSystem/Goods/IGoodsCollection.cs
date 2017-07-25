using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    public interface IGoodsCollection
    {
        event Action<GoodsCollection, GoodType> PendingTransactionsChanged;
        event Action<GoodsCollection, GoodType> GoodChanged;
        int this[GoodType type] { get; set; }
        List<Good> Goods { get; }
        List<Transaction> PendingTransactions { get; }
        bool CanProcessTransaction(Transaction transaction);
        bool CanProcessTransaction(IList<Transaction> transactions);
        bool CanProcessCost(Cost cost);
        bool CanProcessCost(IList<Cost> costs);
        bool TryAdd(Transaction transaction);
        bool TryAdd(IList<Transaction> transactions);
        void Remove(Transaction transaction);
        void Remove(IList<Transaction> transactions);
        bool ContainsGoodOfType(GoodType type);
        void ProcessAllTransactions();
        void ResolveEdgeCases();
    }
}
