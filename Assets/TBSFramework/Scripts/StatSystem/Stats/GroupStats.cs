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
                
                switch (statType.Value)
                {
                    case 0:
                        return (base[StatType.StrengthPerPop] * group.Goods.Population) + base[StatType.Strength];
                    case 11:
                        return (base[StatType.GroupFoodRatePerPop] * group.Goods.Population) + base[StatType.GroupFoodRate];
                    case 12:
                        return (base[StatType.GroupWoodRatePerPop] * group.Goods.Population) + base[StatType.GroupWoodRate];
                    case 47:
                        return (base[StatType.GroupLaborPerPop] * group.Goods.Population) + base[StatType.GroupLaborRate];
                    default:
                        return base[statType];
                }

            }
        }
    }
}
