using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CostType : StatType
    {

        // Clear Module Stats
        
        public static readonly CostType ClearLandWorkCost = new CostType(220, "Clear Land Cost",GoodType.Work,"LandClearingActivity");
        public static readonly CostType ClearLandFoodBonus = new CostType(221, "Clear Land Food Bonus", GoodType.Food, "LandClearingActivity");
        public static readonly CostType ClearLandWoodBonus = new CostType(222, "Clear Land Wood Bonus",GoodType.Wood, "LandClearingActivity");
        public static readonly CostType ClearLandStoneBonus = new CostType(223, "Clear Land Stone Bonus", GoodType.Stone, "LandClearingActivity");
        public static readonly CostType ClearLandGoldBonus = new CostType(224, "Clear Land Gold Bonus", GoodType.Gold, "LandClearingActivity");
        public static readonly CostType ClearLandTime = new CostType(225, "Clear Land Time", GoodType.Turns, "LandClearingActivity");

        // Work Land Module Stats
        public static readonly CostType WorkLandWorkCost = new CostType(230, "Work Land Cost", GoodType.Work, "WorkLandActivity");

        // Build Stats
        public static readonly CostType BuildWorkCost = new CostType(240, "Build Work Cost", GoodType.Work, "BuildActivity");
        public static readonly CostType BuildWoodCost = new CostType(241, "Build Wood Cost", GoodType.Wood, "BuildActivity");
        public static readonly CostType BuildStoneCost = new CostType(242, "Build Stone Cost", GoodType.Stone, "BuildActivity");
        public static readonly CostType BuildGoldCost = new CostType(243, "Build Gold", GoodType.Gold, "BuildActivity");
        public static readonly CostType BuildTime = new CostType(244, "Build Time", GoodType.Turns, "BuildActivity");


        // Raze Module Stats
        public static readonly CostType RazeWorkCost = new CostType(250, "Raze Work Costs", GoodType.Work, "RazeActivity");
        public static readonly CostType RazeTime = new CostType(251, "Raze Time", GoodType.Turns, "RazeActivity");

        // Farm Module Stats
        public static readonly CostType FarmCultivateWorkCost = new CostType(261, "Farm Cultivate Work Cost", GoodType.Work, "WorkImprovementActivity");
        public static readonly CostType FarmHarvestWorkCost = new CostType(262, "Farm Harvest Work Cost", GoodType.Work, "WorkImprovementActivity");
        public static readonly CostType FarmGrowTime = new CostType(263, "Farm Grow Time", GoodType.Turns, "WorkImprovementActivity");
        public static readonly CostType FarmFallowTime = new CostType(364, "Farm Fallow Time", GoodType.Turns, "WorkImprovementActivity");


        // Work Improvement Module Stats
        public static readonly CostType ImprovementWorkCost = new CostType(253, "Improvement Work Cost", GoodType.Work, "WorkImprovementActivity");


        #region StatType Object

        [SerializeField]
        GoodType _good;
        public GoodType Good { get { return _good; } }

        [SerializeField]
        string _activity;
        public string Activity { get { return _activity; } }

        public CostType() { }

        protected CostType(int value, string displayName, GoodType good, string activity) : base(value, displayName)
        {
            _activity = activity;
            _good = good;
            _abbreviation = "";
        }


        #endregion

    }
}
