using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CostType : StatType
    {
        public static readonly CostType NullCost = new CostType(-1, "Null Cost", GoodType.None, CategoryType.None);


        // Clear Module Stats
        public static readonly CostType ClearLandLaborCost = new CostType(220, "Clear Land Labor",GoodType.Labor, CategoryType.Clearing);
        public static readonly CostType ClearLandFoodBonus = new CostType(221, "Clear Land Food Bonus", GoodType.Food, CategoryType.Clearing);
        public static readonly CostType ClearLandWoodBonus = new CostType(222, "Clear Land Wood Bonus",GoodType.Wood, CategoryType.Clearing);
        public static readonly CostType ClearLandStoneBonus = new CostType(223, "Clear Land Stone Bonus", GoodType.Stone, CategoryType.Clearing);
        public static readonly CostType ClearLandGoldBonus = new CostType(224, "Clear Land Gold Bonus", GoodType.Gold, CategoryType.Clearing);
        public static readonly CostType ClearLandTime = new CostType(225, "Clear Land Time", GoodType.Turns, CategoryType.Clearing);

        // Build Stats
        public static readonly CostType BuildLaborCost = new CostType(240, "Build Labor", GoodType.Labor, CategoryType.Building);
        public static readonly CostType BuildWoodCost = new CostType(241, "Build Wood Cost", GoodType.Wood, CategoryType.Building);
        public static readonly CostType BuildStoneCost = new CostType(242, "Build Stone Cost", GoodType.Stone, CategoryType.Building);
        public static readonly CostType BuildGoldCost = new CostType(243, "Build Gold", GoodType.Gold, CategoryType.Building);
        public static readonly CostType BuildTime = new CostType(244, "Build Time", GoodType.Turns, CategoryType.Building);


        // Raze Module Stats
        public static readonly CostType RazeLaborCost = new CostType(250, "Raze Labor", GoodType.Labor, CategoryType.Razing);
        public static readonly CostType RazeTime = new CostType(251, "Raze Time", GoodType.Turns, CategoryType.Razing);

        // Farm Module Stats
        public static readonly CostType FarmLaborCost = new CostType(261, "Farm Labor", GoodType.Labor, CategoryType.Farming);
        public static readonly CostType FarmGrowTime = new CostType(263, "Farm Grow Time", GoodType.Turns, CategoryType.Farming);
        public static readonly CostType FarmFallowTime = new CostType(364, "Farm Fallow Time", GoodType.Turns, CategoryType.Farming);


        // Work Improvement Module Stats
        public static readonly CostType WorkLaborCost = new CostType(253, "Work Labor", GoodType.Labor, CategoryType.Working);


        #region StatType Object

        public enum CategoryType
        {
            None = 0,
            Building = 1,
            Clearing = 2,
            Razing = 3,
            Farming = 4,
            Working = 5
        }


        [SerializeField]
        GoodType _good;
        public GoodType Good { get { return _good; } }

        [SerializeField]
        CategoryType _category;
        public CategoryType Category { get { return _category; } }

        public CostType() { }

        protected CostType(int value, string displayName, GoodType good, CategoryType category) : base(value, displayName)
        {
            _category = category;
            _good = good;
            _abbreviation = "";
        }


        #endregion

    }
}
