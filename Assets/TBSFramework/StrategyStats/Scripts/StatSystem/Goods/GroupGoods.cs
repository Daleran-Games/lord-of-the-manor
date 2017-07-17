using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class GroupGoods : GoodsCollection
    {
        [System.NonSerialized]
        protected Group owner;

        protected int food;
        public virtual Good Food { get { return new Good(GoodType.Food, food); } }

        protected int wood;
        public virtual Good Wood { get { return new Good(GoodType.Wood, wood); } }

        protected int stone;
        public virtual Good Stone { get { return new Good(GoodType.Stone, stone); } }

        protected int gold;
        public virtual Good Gold { get { return new Good(GoodType.Gold, gold); } }

        protected int population;
        public virtual Good Population { get { return new Good(GoodType.Population, population); } }

        protected int work;
        public virtual Good Work { get { return new Good(GoodType.Work, work); } }


        public GroupGoods (Group unit) : base()
        {
            owner = unit;
        }

        public override Good this[GoodType type]
        {
            get
            {
                switch(type)
                {
                    case GoodType.Food:
                        return Food;
                    case GoodType.Wood:
                        return Wood;
                    case GoodType.Stone:
                        return Stone;
                    case GoodType.Gold:
                        return Gold;
                    case GoodType.Population:
                        return Population;
                    case GoodType.Work:
                        return Work;
                    default:
                        return null;
                }
            }
            set
            {
                switch (type)
                {
                    case GoodType.Food:
                        food = value.Value;
                        break;
                    case GoodType.Wood:
                        wood = value.Value;
                        break;
                    case GoodType.Stone:
                        stone = value.Value;
                        break;
                    case GoodType.Gold:
                        gold = value.Value;
                        break;
                    case GoodType.Population:
                        population = value.Value;
                        break;
                    case GoodType.Work:
                        work = value.Value;
                        break;
                }
            }
        }

        public override Good[] GetAllGoods()
        {
            return new Good[]
            {
                Food,
                Wood,
                Stone,
                Gold,
                Population,
                Work
            };
        }

        public override bool ContainsGoodOfType (GoodType type)
        {
            if (
                type == GoodType.Food ||
                type == GoodType.Wood ||
                type == GoodType.Stone ||
                type == GoodType.Gold ||
                type == GoodType.Work ||
                type == GoodType.Population 
                )
                return true;
            else
                return false;
        }

        public override void ResolveEdgeCases()
        {

        }
    }
}
