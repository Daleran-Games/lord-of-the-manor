﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class NullUnitGoods : GroupGoods
    {

        public override Good Food { get { return new Good(GoodType.Food, 0); } }
        public override Good Wood { get { return new Good(GoodType.Wood, 0); } }
        public override Good Stone { get { return new Good(GoodType.Stone, 0); } }
        public override Good Gold { get { return new Good(GoodType.Gold, 0); } }
        public override Good Population { get { return new Good(GoodType.Population, 0); } }
        public override Good Labor { get { return new Good(GoodType.Labor, 0); } }


        public NullUnitGoods(Group unit) : base (unit)
        {

        }

        public override int this[GoodType type]
        {
            get
            {
                return 0;
            }
            protected set
            {

            }
        }

        public override List<Good> Goods
        {
            get
            {
                return new List<Good>(0);
            }
        }

        public override bool ContainsGoodOfType(GoodType type)
        {
            return false;
        }

        public override void ResolveEdgeCases()
        {

        }

        public override bool CanProcessNow(Transaction transaction)
        {
            return false;
        }

        public override bool CanProcessNow(IList<Transaction> transactions)
        {
            return false;
        }

        public override void ProcessNow(Transaction transaction)
        {

        }

        public override void ProcessNow(IList<Transaction> transactions)
        {

        }

        public override void AddFuture(Transaction transaction)
        {

        }

        public override void AddFuture(IList<Transaction> transactions)
        {

        }
        public override void RemoveFuture(Transaction transaction)
        {

        }

        public override void RemoveFuture(IList<Transaction> transactions)
        {

        }

        public override void ProcessAllPendingTransactions()
        {

        }
    }
}

