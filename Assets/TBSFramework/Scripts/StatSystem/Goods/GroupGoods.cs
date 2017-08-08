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
                        return Food.Value;
                    case GoodType.Wood:
                        return Wood.Value;
                    case GoodType.Stone:
                        return Stone.Value;
                    case GoodType.Gold:
                        return Gold.Value;
                    case GoodType.Population:
                        return Population.Value;
                    case GoodType.Labor:
                        return Labor.Value;
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
                        OnGoodChanged(this, Food.Type);
                        break;
                    case GoodType.Wood:
                        wood = value;
                        OnGoodChanged(this, Wood.Type);
                        break;
                    case GoodType.Stone:
                        stone = value;
                        OnGoodChanged(this, Stone.Type);
                        break;
                    case GoodType.Gold:
                        gold = value;
                        OnGoodChanged(this, Gold.Type);
                        break;
                    case GoodType.Population:
                        population = value;
                        OnGoodChanged(this, Population.Type);
                        break;
                    case GoodType.Labor:
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
                        this[GoodType.Population]--;
                    }
                }
                this[GoodType.Food] = 0;
            } else if (food > owner.Stats[StatType.MaxFood])
            {
                AddFuture(new Transaction(GoodType.Food, owner.Stats[StatType.MaxFood] - food, "Spoilage from not enoguh storage"));
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
                    }
                    this[GoodType.Wood] = 0;
                } else
                    this[GoodType.Wood] = 0;
            } else if (wood > owner.Stats[StatType.MaxWood])
            {
                AddFuture(new Transaction(GoodType.Wood, owner.Stats[StatType.MaxWood] - wood, "Waste from not enoguh storage"));
            }
        }

        void CheckStone ()
        {
            if (stone < 0)
                this[GoodType.Stone] = 0;
            else if (stone > owner.Stats[StatType.MaxStone])
            {
                AddFuture(new Transaction(GoodType.Stone, owner.Stats[StatType.MaxStone] - stone, "Waste from not enoguh storage"));
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
                AddFuture(new Transaction(GoodType.Population, owner.Stats[StatType.MaxPopulation] - population, "Emigration from overpopulation"));
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
            AddFuture(new Transaction(GoodType.Labor, owner.Stats[StatType.GroupLaborRate], "Work"));
        }
    }
}
