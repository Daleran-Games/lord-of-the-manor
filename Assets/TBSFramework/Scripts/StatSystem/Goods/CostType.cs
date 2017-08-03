using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CostType : StatType
    {
        public static readonly CostType NullCost = new CostType(-1, "Null Cost", GoodType.None, CommandType.None);


        // Clear Forest Stats
        public static readonly CostType ClearLandCost = new CostType(220, "Clear Land Cost",GoodType.Labor, CommandType.Pause);
        public static readonly CostType ClearLandYieldRate = new CostType(221, "Clear Land Yield Rate", GoodType.Food, CommandType.Pause);
        public static readonly CostType ClearLandTime = new CostType(225, "Clear Land Time", GoodType.Turns, CommandType.Pause);

        // Build Stats
        public static readonly CostType BuildLaborCost = new CostType(240, "Build Labor", GoodType.Labor, CommandType.Pause);
        public static readonly CostType BuildWoodCost = new CostType(241, "Build Wood Cost", GoodType.Wood, CommandType.Pause);
        public static readonly CostType BuildStoneCost = new CostType(242, "Build Stone Cost", GoodType.Stone, CommandType.Pause);
        public static readonly CostType BuildGoldCost = new CostType(243, "Build Gold", GoodType.Gold, CommandType.Pause);
        public static readonly CostType BuildTime = new CostType(244, "Build Time", GoodType.Turns, CommandType.Pause);


        // Raze Module Stats
        public static readonly CostType RazeLaborCost = new CostType(250, "Raze Labor", GoodType.Labor, CommandType.Cancel);
        public static readonly CostType RazeTime = new CostType(251, "Raze Time", GoodType.Turns, CommandType.Cancel);

        // Farm Module Stats
        public static readonly CostType FarmLaborCost = new CostType(261, "Farm Labor", GoodType.Labor, CommandType.Working);
        public static readonly CostType FarmGrowTime = new CostType(263, "Farm Grow Time", GoodType.Turns, CommandType.Working);
        public static readonly CostType FarmFallowTime = new CostType(364, "Farm Fallow Time", GoodType.Turns, CommandType.Working);


        // Work Improvement Module Stats
        public static readonly CostType WorkLaborCost = new CostType(253, "Work Labor", GoodType.Labor, CommandType.Working);


        #region StatType Object

        [SerializeField]
        GoodType _good;
        public GoodType Good { get { return _good; } }

        [SerializeField]
        CommandType _activity;
        public CommandType Activity { get { return _activity; } }

        public CostType() { }

        protected CostType(int value, string displayName, GoodType good, CommandType activity) : base(value, displayName)
        {
            _activity = activity;
            _good = good;
            _abbreviation = "";
        }


        #endregion

    }
}
