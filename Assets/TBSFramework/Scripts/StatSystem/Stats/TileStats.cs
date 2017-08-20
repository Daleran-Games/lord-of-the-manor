using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TileStats : StatCollection
    {
        protected Group owner;
        public Group Owner
        {
            get { return owner; }
            set
            {
                if (owner != null)
                    owner.TileModifiers.StatModified -= RaiseStatModified;

                owner = value;

                if (owner != null)
                    owner.TileModifiers.StatModified += RaiseStatModified;
            }
        }


        public TileStats(Group owner) : base()
        {
            this.Owner = owner;
        }

        public override int this[StatType statType]
        {
            get
            {
                switch (statType.Name)
                {
                    case "Farming Food Rate":
                        return (Get(StatType.FoodYield) * Get(StatType.FarmingFoodPerYield)) + Get(StatType.FarmingFoodRate);
                    case "Logging Wood Rate":
                        return (Get(StatType.WoodYield) * Get(StatType.LoggingWoodPerYield)) + Get(StatType.LoggingWoodRate);
                    case "Quarrying Stone Rate":
                        return (Get(StatType.StoneYield) * Get(StatType.QuarryingStonePerYield)) + Get(StatType.QuarryingStoneRate);
                    default:
                        return Get(statType);

                }
            }
        }

        protected int Get(StatType statType)
        {
            if (totals.ContainsKey(statType) || owner.TileModifiers.Contains(statType))
                return totals[statType] + owner.TileModifiers[statType];
            else
                return 0;
        }

        public override List<StatType> Types
        {
            get
            {
                List<StatType> types = new List<StatType>(base.Types);
                types.AddRange(owner.TileModifiers.Types);
                return types;
            }
        }

        public override List<Modifier> GetAllOfType(StatType statType)
        {
            if (modifiers.ContainsKey(statType) || owner.TileModifiers.Contains(statType))
            {
                List<Modifier> mods = new List<Modifier>(modifiers[statType]);
                mods.AddRange(owner.TileModifiers.GetAllOfType(statType));
                return mods;
            }
            else
                return new List<Modifier>(0);
        }

        public override List<Modifier> GetAll()
        {
            List<Modifier> mods = new List<Modifier>();
            foreach (KeyValuePair<StatType, List<Modifier>> m in modifiers)
            {
                mods.AddRange(m.Value);
            }
            mods.AddRange(owner.TileModifiers.GetAll());

            return mods;
        }

        protected override void RaiseStatModified(IStatCollection<StatType> stats, StatType statType)
        {
            base.RaiseStatModified(stats, statType);

            if (statType == StatType.FoodYield)
                base.RaiseStatModified(stats, StatType.FarmingFoodRate);
            else if (statType == StatType.FarmingFoodPerYield)
                base.RaiseStatModified(stats, StatType.FarmingFoodRate);
            else if (statType == StatType.WoodYield)
                base.RaiseStatModified(stats, StatType.LoggingWoodRate);
            else if (statType == StatType.LoggingWoodPerYield)
                base.RaiseStatModified(stats, StatType.LoggingWoodRate);
            else if (statType == StatType.StoneYield)
                base.RaiseStatModified(stats, StatType.QuarryingStoneRate);
            else if (statType == StatType.QuarryingStonePerYield)
                base.RaiseStatModified(stats, StatType.QuarryingStoneRate);
        }

    }
}
