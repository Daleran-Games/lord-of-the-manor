using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class StatType : Enumeration
    {
        #region StatTypes

        //Next stat is 25

        public static readonly StatType NullStat = new StatType(-1, "Null", "NL");

        //Unit Stats
        public static readonly StatType Strength = new StatType(0, "Strength","STR");
        public static readonly StatType StrengthPerPop = new StatType(1, "Strength Per Population","STR/P");
        public static readonly StatType AttackCost = new StatType(22, "Attack Cost", "AC");
        public static readonly StatType Happiness = new StatType(2, "Happiness", "HAP");
        public static readonly StatType MaxPopulation = new StatType(3, "Max Population", "POP");
        public static readonly StatType MaxFood = new StatType(4, "Max Food", "MX FD");
        public static readonly StatType MaxWood = new StatType(5, "Max Wood", "MX WD");
        public static readonly StatType MaxStone = new StatType(6, "Max Stone", "MX ST");
        public static readonly StatType MaxActionPoints = new StatType(7, "Max Action Points","MX AP");
        public static readonly StatType GroupFoodRatePerPop = new StatType(8, "Food Use Per Turn Per Population","FD/T/P");
        public static readonly StatType GroupWoodRatePerPop = new StatType(9, "Wood Use In Winter Per Population", "WD/W/P");
        public static readonly StatType GroupFoodRate = new StatType(23, "Food Use Per Turn", "FD/T");
        public static readonly StatType GroupWoodRate = new StatType(24, "Wood Use In Winter", "WD/W");
        public static readonly StatType GroupBirthRate = new StatType(10, "Birth Rate", "BR");
        public static readonly StatType GroupDeathRate = new StatType(11, "Death Rate", "DR");
        public static readonly StatType GroupStarvationRate = new StatType(12, "Starvation Rate", "SR");
        public static readonly StatType GroupFreezeRate = new StatType(13, "Freeze Rate", "FR");


        //Tile Stats
        public static readonly StatType MovementCost = new StatType(14, "Movement Cost", "MC");
        public static readonly StatType DefenseBonus = new StatType(15, "Defense Bonus", "DB");


        //Land Stats
        public static readonly StatType FoodYield = new StatType(16, "Food Yield", "FY");
        public static readonly StatType WoodYield = new StatType(18, "Wood Yield", "WY");
        public static readonly StatType StoneYield = new StatType(19, "Stone Yield", "SY");
        public static readonly StatType GoldYield = new StatType(20, "Gold Yield", "GY");

        //Improvement Stats
        public static readonly StatType MaxCondition = new StatType(21, "Max Condition", "MX CD");

        //Work Module Stats


        // Work Land Module Stats


        // Clear Module Stats
        public static StatType ClearLandTurns = new StatType(25, "Clear Land Turns", "CLT");
        public static StatType ClearLandCosts = new StatType(26, "Clear Land Cost","CLC");
        public static StatType ClearLandBonus = new StatType(27, "Clear Land Bonus", "CLB");


        // Raze Module Stats


        // Farm Module Stats


        // Work Improvement Module Stats


        #endregion


        #region StatType Object

        string _abbreviation;
        public string Abbreviation { get { return _abbreviation; } }
            
        public StatType () { }

        protected StatType (int value, string displayName, string abbreviation) : base (value,displayName)
        {
            _abbreviation = abbreviation;
        }
# endregion

    }
}

