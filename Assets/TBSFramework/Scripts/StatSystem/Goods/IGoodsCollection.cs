using System;
using System.Collections.Generic;

namespace DaleranGames.TBSFramework
{
    public interface IGoodsCollection
    {
        event Action<GoodsCollection, GoodType> FutureTransactionsChanged;
        event Action<GoodsCollection, GoodType> GoodChanged;
        int this[GoodType type] { get; set; }
        List<Good> Goods { get; }
        List<Transaction> PendingTransactions { get; }

        bool CanProcessNow(Transaction transaction);
        bool CanProcessNow(IList<Transaction> transactions);

        void ProcessNow(Transaction transaction);
        void ProcessNow(IList<Transaction> transactions);

        void AddFuture(Transaction transaction);
        void AddFuture(IList<Transaction> transactions);

        void RemoveFuture(Transaction transaction);
        void RemoveFuture(IList<Transaction> transactions);


        bool ContainsGoodOfType(GoodType type);
        void ProcessAllPendingTransactions();
        void ResolveEdgeCases();
    }
}
