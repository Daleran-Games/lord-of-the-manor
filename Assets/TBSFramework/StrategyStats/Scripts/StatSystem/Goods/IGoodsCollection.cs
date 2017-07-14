using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public interface IGoodsCollection
    {
        System.Action<Good> GoodDepleted { get; }
        Good this[GoodType type] { get; }
        Good[] GetAllGoods();
        Transaction[] PendingTransactions { get; }
        bool CanProcessTransaction(Transaction transaction);
        bool CanProcessTransaction(Transaction[] transactions);
        bool Add(Transaction transaction);
        bool Add(Transaction[] transactions);
        void Remove(Transaction transaction);
        void Remove(Transaction[] transactions);
        void Set(Transaction transaction);
        bool ContainsGoodOfType(GoodType type);
        void ProcessTransactions();
        void ResolveEdgeCases();
    }
}
