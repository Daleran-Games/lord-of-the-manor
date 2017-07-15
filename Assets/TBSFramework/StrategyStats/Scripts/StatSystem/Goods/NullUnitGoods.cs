using System.Collections;
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
        public override Good Work { get { return new Good(GoodType.Work, 0); } }


        public NullUnitGoods(Group unit) : base (unit)
        {

        }

        public override Good this[GoodType type]
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        public override Good[] GetAllGoods()
        {
            return new Good[0];
        }

        public override bool ContainsGoodOfType(GoodType type)
        {
            return false;
        }

        public override void ResolveEdgeCases()
        {

        }

        public override bool CanProcessTransaction(Transaction transaction)
        {
            return false;
        }

        public override bool CanProcessTransaction(Transaction[] transactions)
        {
            return false;
        }

        public override bool TryAdd(Transaction transaction)
        {
            return false;
        }

        public override bool TryAdd(Transaction[] transactions)
        {
            return false;
        }

        public override void Remove(Transaction transaction)
        {

        }

        public override void Remove(Transaction[] transactions)
        {

        }

        public override void ProcessTransactions()
        {

        }
    }
}

