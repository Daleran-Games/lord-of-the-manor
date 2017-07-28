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
        protected int labor;
        public virtual Good Labor { get { return new Good(GoodType.Labor, labor); } }


        public GroupGoods (Group unit) : base()
        {
            owner = unit;
            food = 0;
            wood = 0;
            stone = 0;
            gold = 0;
            population = 0;
            labor = 0;
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
                    case GoodType.Labor:
                        return Labor;
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
                    case GoodType.Labor:
                        labor = value;
                        OnGoodChanged(this, Labor);
                        break;
                }
            }
        }

        public override List<Good>Goods
        {
            get
            {
                return new List<Good>
                {
                Food,
                Wood,
                Stone,
                Gold,
                Population,
                Labor
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
                type == GoodType.Labor ||
                type == GoodType.Population 
                )
                return true;
            else
                return false;
        }

        public override bool CanProcessCost(Cost cost)
        {
            return CanProcessTransaction(cost.GetTransactionWithModifiers(owner.Stats));
        }

        public override void ResolveEdgeCases()
        {
            CheckFood();
            CheckWood();
            CheckStone();
            CheckGold();
            CheckPopulation();
            CheckWork();
        }

        void CheckFood()
        {
            if (food < 0)
            {
                for (int f = food; f <= 0; f++)
                {
                    if (Random.Bool(owner.Stats[StatType.GroupStarvationRate]))
                    {
                        this[GoodType.Population]--;
                    }
                    this[GoodType.Food]++;
                }
            } else if (food > owner.Stats[StatType.MaxFood])
            {
                Add(new Transaction(GoodType.Food, owner.Stats[StatType.MaxFood] - food, false, "Spoilage"));
            }
        }

        void CheckWood ()
        {
            if (wood < 0)
            {
                if (TurnManager.Instance.CurrentTurn == TurnManager.Instance.Fall)
                {
                    for (int w=wood; w <= 0; w++)
                    {
                        if (Random.Bool(owner.Stats[StatType.GroupFreezeRate]))
                            this[GoodType.Population]--;

                        this[GoodType.Wood]++;
                    }
                } else
                    this[GoodType.Wood] = 0;
            } else if (wood > owner.Stats[StatType.MaxWood])
            {
                Add(new Transaction(GoodType.Wood, owner.Stats[StatType.MaxWood] - wood, false, "Wood Waste"));
            }
        }

        void CheckStone ()
        {
            if (stone < 0)
                this[GoodType.Stone] = 0;
            else if (stone > owner.Stats[StatType.MaxStone])
            {
                Add(new Transaction(GoodType.Stone, owner.Stats[StatType.MaxStone] - stone, false, "Stone Waste"));
            }
        }

        void CheckGold()
        {

        }

        void CheckPopulation ()
        {
            if (population < 0)
                this[GoodType.Population] = 0;
            else if (population > owner.Stats[StatType.MaxPopulation])
            {
                Add(new Transaction(GoodType.Population, owner.Stats[StatType.MaxPopulation] - population, false, "Overpopulation"));
            } else
            {
                int births = 0;
                int deaths = 0;

                for (int p = 0; p < population; p++)
                {
                    if (Random.Bool(owner.Stats[StatType.GroupBirthRate]))
                        births++;

                    if (Random.Bool(owner.Stats[StatType.GroupDeathRate]))
                        deaths++;
                }

                if (births > 0)
                    Add(new Transaction(GoodType.Population, births, false, "Births"));

                if (deaths > 0)
                    Add(new Transaction(GoodType.Population, -deaths, false, "Deaths"));

            }
        }

        void CheckWork ()
        {
            if (labor < 0)
                this[GoodType.Labor] = 0;
        }
    }
}
