using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class StatType : Enumeration
    {
        #region StatTypes

        public static readonly StatType NullStat = new StatType(-1, "Null");


        //Unit Stats
        public static readonly StatType Strength = new StatType(0, "Strength");
        public static readonly StatType StrengthPerPop = new StatType(1, "Strength Per Population");
        public static readonly StatType MaxActionPoints = new StatType(2, "Max Action Points");
        public static readonly StatType Happiness = new StatType(3, "Happiness");
        public static readonly StatType AttackCost = new StatType(4, "Attack Cost");
        public static readonly StatType GroupLevy = new StatType(5, "Levy Percent");

        public static readonly StatType MaxPopulation = new StatType(10, "Max Population");
        public static readonly StatType MaxFood = new StatType(11, "Max Food");
        public static readonly StatType MaxWood = new StatType(12, "Max Wood");
        public static readonly StatType MaxStone = new StatType(13, "Max Stone");

        public static readonly StatType GroupFoodRatePerPop = new StatType(14, "Food Use Per Turn Per Population");
        public static readonly StatType GroupFoodRate = new StatType(15, "Food Use Per Turn");
        public static readonly StatType GroupWoodRate = new StatType(17, "Wood Use In Winter");
        public static readonly StatType GroupLaborPerPop = new StatType(18, "Labor Per Turn Per Population");
        public static readonly StatType GroupLaborRate = new StatType(19, "Labor Per Turn");

        public static readonly StatType GroupBirthRate = new StatType(20, "Birth Rate");
        public static readonly StatType GroupDeathRate = new StatType(21, "Death Rate");
        public static readonly StatType GroupStarvationRate = new StatType(22, "Starvation Rate");
        public static readonly StatType GroupFreezeRate = new StatType(23, "Freeze Rate");
        public static readonly StatType StartingGold = new StatType(24, "Starting Gold");
        public static readonly StatType LandCostModifier = new StatType(25, "Land Cost Modifier");


        //Tile Stats
        public static readonly StatType DefenseBonus = new StatType(30, "Defense Bonus");
        public static readonly StatType MovementCost = new StatType(31, "Movement Cost");
        public static readonly StatType LandValue = new StatType(33, "Land Value");


        //Land Stats
        public static readonly StatType FoodYield = new StatType(40, "Food Yield");
        public static readonly StatType WoodYield = new StatType(41, "Wood Yield");
        public static readonly StatType StoneYield = new StatType(42, "Stone Yield");
        public static readonly StatType GoldYield = new StatType(43, "Gold Yield");

        //Feature Stats
        public static readonly StatType MaxCondition = new StatType(50, "Max Condition");

        //Building Stats
        public static readonly StatType BuildLaborCost = new StatType(200, "Build Labor");
        public static readonly StatType BuildWoodCost = new StatType(201, "Build Wood Cost");
        public static readonly StatType BuildStoneCost = new StatType(202, "Build Stone Cost");
        public static readonly StatType BuildGoldCost = new StatType(203, "Build Gold");
        public static readonly StatType BuildTime = new StatType(204, "Build Time");

        //Razing Stats
        // Raze Module Stats
        public static readonly StatType RazeLaborCost = new StatType(210, "Raze Labor");
        public static readonly StatType RazeTime = new StatType(211, "Raze Time");

        //Farming Stats
        public static readonly StatType FarmLaborCost = new StatType(220, "Farm Labor");
        public static readonly StatType FarmGrowTime = new StatType(221, "Farm Grow Time");
        public static readonly StatType FarmCycleTime = new StatType(222, "Farm Cycle Time");
        public static readonly StatType FarmingFoodPerYield = new StatType(223, "Farming Food Per Tile Yield");
        public static readonly StatType FarmingFoodRate = new StatType(224, "Farming Food Rate");


        //Logging Stats
        public static readonly StatType LoggingLaborCost = new StatType(230, "Logging Labor");
        public static readonly StatType LoggingTime = new StatType(231, "Logging Time");
        public static readonly StatType LoggingWoodPerYield = new StatType(232, "Logging Wood Per Tile Yield");
        public static readonly StatType LoggingWoodRate = new StatType(233, "Logging Wood Rate");

        //Quarrying Stats
        public static readonly StatType QuarryLaborCost = new StatType(240, "Quarry Labor");
        public static readonly StatType QuarryTime = new StatType(241, "Quarry Time");
        public static readonly StatType QuarryingStonePerYield = new StatType(242, "Quarrying Stone Per Tile Yield");
        public static readonly StatType QuarryingStoneRate = new StatType(243, "Quarrying Stone Rate");

        //Need to implement

        /*
        //Fishing Stats
        public static readonly StatType LoggingLaborCost = new StatType(230, "Logging Labor");
        public static readonly StatType LoggingTime = new StatType(231, "Logging Time");
        public static readonly StatType LoggingRate = new StatType(232, "Logging Rate");

        //Hunting Stats
        public static readonly StatType LoggingLaborCost = new StatType(230, "Logging Labor");
        public static readonly StatType LoggingTime = new StatType(231, "Logging Time");
        public static readonly StatType LoggingRate = new StatType(232, "Logging Rate");

        //Herding Stats
        public static readonly StatType HerdRate = new StatType(60, "Herd Per Food Yield");
        public static readonly StatType MaxHerd = new StatType(60, "Max Herd Population");
        */





        #endregion


        #region StatType Object

        [SerializeField]
        protected string _statIcon;
        public string Icon { get { return _statIcon; } }

        [SerializeField]
        protected string _description;
        public string Description { get { return _description; } }

        public enum StatScope
        {
            None = 0,
            Group = 1,
            Tile = 2
        }

        [SerializeField]

        public StatType () { }

        protected StatType(int value, string displayName) : base(value, displayName)
        {
            _statIcon = "<sprite name=\"Icon_16_Gen_Question\">";
        }

        protected StatType (int value, string displayName, string statIcon, string description) : base (value,displayName)
        {
            _statIcon = statIcon;
            _description = description;
        }

# endregion

    }
}

