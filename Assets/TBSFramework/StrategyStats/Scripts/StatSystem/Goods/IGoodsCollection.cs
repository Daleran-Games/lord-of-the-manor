using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public interface IGoodsCollection
    {
        event System.Action<Good> GoodDepleted;
        Good this[GoodType type] { get; set; }
        Good[] GetAllGoods();
        Transaction[] PendingTransactions { get; }
        bool CanProcessTransaction(Transaction transaction);
        bool CanProcessTransaction(Transaction[] transactions);
        bool TryAdd(Transaction transaction);
        bool TryAdd(Transaction[] transactions);
        void Remove(Transaction transaction);
        void Remove(Transaction[] transactions);
        bool ContainsGoodOfType(GoodType type);
        void ProcessTransactions();
        void ResolveEdgeCases();
    }
}
