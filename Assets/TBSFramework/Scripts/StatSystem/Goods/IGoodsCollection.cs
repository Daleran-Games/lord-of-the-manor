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
        Good[] Goods { get; }
        Transaction[] PendingTransactions { get; }
        bool CanProcessTransaction(Transaction transaction);
        bool CanProcessTransaction(Transaction[] transactions);
        bool TryAdd(Transaction transaction);
        bool TryAdd(Transaction[] transactions);
        void Remove(Transaction transaction);
        void Remove(Transaction[] transactions);
        bool ContainsGoodOfType(GoodType type);
        void ProcessAllTransactions();
        void ResolveEdgeCases();
    }
}
