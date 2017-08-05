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
        }


        public override int this[StatType statType]
        {
            get
            {
                
                switch (statType.DisplayName)
                {
                    case "Strength":
                        return (base[StatType.StrengthPerPop] * group.Goods.Population.Value) + base[StatType.Strength];
                    case "Food Use Per Turn":
                        return (base[StatType.GroupFoodRatePerPop] * group.Goods.Population.Value) + base[StatType.GroupFoodRate];
                    case "Wood Use In Winter":
                        return (base[StatType.GroupWoodRatePerPop] * group.Goods.Population.Value) + base[StatType.GroupWoodRate];
                    case "Labor Per Turn":
                        return (base[StatType.GroupLaborPerPop] * group.Goods.Population.Value) + base[StatType.GroupLaborRate];
                    default:
                        return base[statType];
                }

            }
        }
    }
}
