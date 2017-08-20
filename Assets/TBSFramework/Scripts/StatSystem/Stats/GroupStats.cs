using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class GroupStats : StatCollection
    {

        protected Group group;
        
        public GroupStats (Group group) : base()
        {
            this.group = group;
            group.Goods.GoodChanged += OnPopulationChange;
        }


        public override int this[StatType statType]
        {
            get
            {
                
                switch (statType.Name)
                {
                    case "Strength":
                        return (base[StatType.StrengthPerPop] * group.Goods.Population.Value) + base[StatType.Strength];
                    case "Food Use Per Turn":
                        return (base[StatType.GroupFoodRatePerPop] * group.Goods.Population.Value) + base[StatType.GroupFoodRate];
                    case "Labor Per Turn":
                        return (base[StatType.GroupLaborPerPop] * group.Goods.Population.Value) + base[StatType.GroupLaborRate];
                    default:
                        return base[statType];
                }

            }
        }

        protected override void RaiseStatModified(IStatCollection<StatType> stats, StatType statType)
        {
            base.RaiseStatModified(stats, statType);

            if (statType == StatType.StrengthPerPop)
                base.RaiseStatModified(stats, StatType.Strength);
            else if (statType == StatType.GroupFoodRatePerPop)
                base.RaiseStatModified(stats, StatType.GroupFoodRate);
            else if (statType == StatType.GroupLaborPerPop)
                base.RaiseStatModified(stats, StatType.GroupLaborRate);

        }

        protected virtual void OnPopulationChange(GoodsCollection col, GoodType type)
        {
            if (type == GoodType.Population)
            {
                RaiseStatModified(this, StatType.Strength);
                RaiseStatModified(this, StatType.GroupFoodRate);
                RaiseStatModified(this, StatType.GroupLaborRate);
            }
        }
    }
}
