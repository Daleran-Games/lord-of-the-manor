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

        [SerializeField]
        protected int food;
        public virtual Good Food { get { return new Good(GoodType.Food, food); } }

        [SerializeField]
        protected int wood;
        public virtual Good Wood { get { return new Good(GoodType.Wood, wood); } }

        [SerializeField]
        protected int stone;
        public virtual Good Stone { get { return new Good(GoodType.Stone, stone); } }

        [SerializeField]
        protected int gold;
        public virtual Good Gold { get { return new Good(GoodType.Gold, gold); } }

        [SerializeField]
        protected int population;
        public virtual Good Population { get { return new Good(GoodType.Population, population); } }

        [SerializeField]
        protected int work;
        public virtual Good Work { get { return new Good(GoodType.Work, work); } }


        public GroupGoods (Group unit) : base()
        {
            owner = unit;
            food = 0;
            wood = 0;
            stone = 0;
            gold = 0;
            population = 0;
            work = 0;
        }

        public override int this[GoodType type]
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
                        return 0;
                }
            }
            set
            {
                switch (type)
                {
                    case GoodType.Food:
                        food = value;
                        OnGoodChanged(this, Food);
                        break;
                    case GoodType.Wood:
                        wood = value;
                        OnGoodChanged(this, Wood);
                        break;
                    case GoodType.Stone:
                        stone = value;
                        OnGoodChanged(this, Stone);
                        break;
                    case GoodType.Gold:
                        gold = value;
                        OnGoodChanged(this, Gold);
                        break;
                    case GoodType.Population:
                        population = value;
                        OnGoodChanged(this, Population);
                        break;
                    case GoodType.Work:
                        work = value;
                        OnGoodChanged(this, Work);
                        break;
                }
            }
        }

        public override Good[]Goods
        {
            get
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
