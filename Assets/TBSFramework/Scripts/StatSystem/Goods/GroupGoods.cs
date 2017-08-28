using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class GroupGoods : GoodsCollection
    {
        protected Group owner;

        //Consider repacing the getters with ints instead of goods. I think int and stat classes might go away entirely.

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
                switch(type.Value)
                {
                    case 0:
                        return Food.Value;
                    case 1:
                        return Wood.Value;
                    case 2:
                        return Stone.Value;
                    case 3:
                        return Gold.Value;
                    case 5:
                        return Population.Value;
                    case 4:
                        return Labor.Value;
                    default:
                        return 0;
                }
            }

            protected set
            {
                switch (type.Value)
                {
                    case 0:
                        food = value;
                        OnGoodChanged(this, Food.Type);
                        break;
                    case 1:
                        wood = value;
                        OnGoodChanged(this, Wood.Type);
                        break;
                    case 2:
                        stone = value;
                        OnGoodChanged(this, Stone.Type);
                        break;
                    case 3:
                        gold = value;
                        OnGoodChanged(this, Gold.Type);
                        break;
                    case 5:
                        population = value;
                        OnGoodChanged(this, Population.Type);
                        break;
                    case 4:
                        labor = value;
                        OnGoodChanged(this, Labor.Type);
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
                        ProcessNow(new Transaction(GoodType.Population, -1, "Death from lack of food"));
                    }
                }
                this[GoodType.Food] = 0;
            } else if (food > owner.Stats[StatType.MaxFood])
            {
                AddFuture(new Transaction(GoodType.Food, owner.Stats[StatType.MaxFood] - food, "Spoilage: Not enough storage"));
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
                            ProcessNow(new Transaction(GoodType.Population, -1, "Death from lack of firewood"));
                    }
                    this[GoodType.Wood] = 0;
                } else
                    this[GoodType.Wood] = 0;
            } 
        }

        void CheckStone ()
        {
            if (stone < 0)
                this[GoodType.Stone] = 0;
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
                AddFuture(new Transaction(GoodType.Population, owner.Stats[StatType.MaxPopulation] - population, "Overpopulation"));
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
                    AddFuture(new Transaction(GoodType.Population, births, "Births"));

                if (deaths > 0)
                    AddFuture(new Transaction(GoodType.Population, -deaths, "Deaths"));

            }
        }

        void CheckWork ()
        {
            AddFuture(new Transaction(GoodType.Labor, owner.Stats[StatType.GroupLaborRate], "Labor from your Clan"));
        }

        public void ResetWork()
        {
            this[GoodType.Labor] = 0;
        }
    }
}
