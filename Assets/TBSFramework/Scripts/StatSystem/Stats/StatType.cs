using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class StatType : Enumeration
    {
        #region StatTypes

        public static readonly StatType NullStat = new StatType(-1, "Null", "NL");

        //Unit Stats
        public static readonly StatType Strength = new StatType(0, "Strength","STR");
        public static readonly StatType StrengthPerPop = new StatType(1, "Strength Per Population","STR/P");
        public static readonly StatType AttackCost = new StatType(2, "Attack Cost", "AC");
        public static readonly StatType Happiness = new StatType(3, "Happiness", "HAP");
        public static readonly StatType MaxPopulation = new StatType(4, "Max Population", "POP");
        public static readonly StatType MaxFood = new StatType(5, "Max Food", "MX FD");
        public static readonly StatType MaxWood = new StatType(6, "Max Wood", "MX WD");
        public static readonly StatType MaxStone = new StatType(7, "Max Stone", "MX ST");
        public static readonly StatType MaxActionPoints = new StatType(8, "Max Action Points","MX AP");
        public static readonly StatType GroupFoodRatePerPop = new StatType(9, "Food Use Per Turn Per Population","FD/T/P");
        public static readonly StatType GroupWoodRatePerPop = new StatType(10, "Wood Use In Winter Per Population", "WD/W/P");
        public static readonly StatType GroupFoodRate = new StatType(11, "Food Use Per Turn", "FD/T");
        public static readonly StatType GroupWoodRate = new StatType(12, "Wood Use In Winter", "WD/W");
        public static readonly StatType GroupBirthRate = new StatType(13, "Birth Rate", "BR");
        public static readonly StatType GroupDeathRate = new StatType(14, "Death Rate", "DR");
        public static readonly StatType GroupStarvationRate = new StatType(15, "Starvation Rate", "SR");
        public static readonly StatType GroupFreezeRate = new StatType(16, "Freeze Rate", "FR");
        public static readonly StatType GroupWorkPerPop = new StatType(23, "Work Per Turn Per Population", "W/T/P");
        public static readonly StatType GroupWorkRate = new StatType(47, "Work Per Turn", "W/T");
        public static readonly StatType GroupLevy = new StatType(48, "Levy Percent", "LY");


        //Tile Stats
        public static readonly StatType MovementCost = new StatType(17, "Movement Cost", "MC");
        public static readonly StatType DefenseBonus = new StatType(18, "Defense Bonus", "DB");


        //Land Stats
        public static readonly StatType FoodYield = new StatType(19, "Food Yield", "FY");
        public static readonly StatType WoodYield = new StatType(20, "Wood Yield", "WY");
        public static readonly StatType StoneYield = new StatType(21, "Stone Yield", "SY");
        public static readonly StatType GoldYield = new StatType(22, "Gold Yield", "GY");

        //Improvement Stats
        public static readonly StatType MaxCondition = new StatType(24, "Max Condition", "MX CD");

        // Clear Module Stats
        public static readonly StatType ClearLandTurns = new StatType(25, "Clear Land Time", "CLT");
        public static readonly StatType ClearLandCost = new StatType(26, "Clear Land Cost", "CLC");
        public static readonly StatType ClearLandBonus = new StatType(27, "Clear Land Bonus", "CLB");

        // Work Land Module Stats
        public static readonly StatType WorkLandCost = new StatType(28, "Work Land Cost", "WLC");
        public static readonly StatType WorkLandFoodRate = new StatType(29, "Work Land Food Rate", "WLF");
        public static readonly StatType WorkLandWoodRate = new StatType(30, "Work Land Wood Rate", "WLW");
        public static readonly StatType WorkLandStoneRate = new StatType(31, "Work Land Stone Rate", "WLS");
        public static readonly StatType WorkLandGoldRate = new StatType(32, "Work Land Gold Rate", "WLG");

        // Build Stats
        public static readonly StatType ConstructionTurns = new StatType(33, "Construction Time");
        public static readonly StatType ConstructionImmediateCosts = new StatType(34, "Construction Costs");
        public static readonly StatType ConstructionPerTurnCosts = new StatType(35, "Construction Per Turn Costs");


        // Raze Module Stats
        public static readonly StatType RazeTurns = new StatType(36, "Raze Time");
        public static readonly StatType RazeCost = new StatType(37, "Raze Cost");
        public static readonly StatType RazeBonus = new StatType(38, "Raze Bonus");

        // Farm Module Stats
        public static readonly StatType FarmTileFoodRate = new StatType(39, "Farm Tile Food Rate");
        public static readonly StatType FarmPlowWorkCost = new StatType(40, "Farm Plow Work Cost");
        public static readonly StatType FarmHarvestWorkCost = new StatType(41, "Farm Harvest Work Cost");
        public static readonly StatType FarmGrowTime = new StatType(42, "Farm Grow Time");
        public static readonly StatType FarmFallowTime = new StatType(43, "Farm Fallow Time");


        // Work Improvement Module Stats
        public static readonly StatType ImprovementWorkCost = new StatType(44, "Work Cost");
        public static readonly StatType ImprovementCost = new StatType(45, "Per Turn Costs");
        public static readonly StatType ImprovementBonus = new StatType(46, "Per Turn Bonuses");

        #endregion


        #region StatType Object

        [SerializeField]
        string _abbreviation;
        public string Abbreviation { get { return _abbreviation; } }
            
        public StatType () { }

        protected StatType(int value, string displayName) : base(value, displayName)
        {
            _abbreviation = "";
        }

        protected StatType (int value, string displayName, string abbreviation) : base (value,displayName)
        {
            _abbreviation = abbreviation;
        }
# endregion

    }
}

