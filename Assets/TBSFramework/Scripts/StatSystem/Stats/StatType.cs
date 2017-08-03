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
        public static readonly StatType MaxActionPoints = new StatType(2, "Max Action Points", "MX AP");
        public static readonly StatType Happiness = new StatType(3, "Happiness", "HAP");
        public static readonly StatType AttackCost = new StatType(4, "Attack Cost", "AC");
        public static readonly StatType GroupLevy = new StatType(5, "Levy Percent", "LY");

        public static readonly StatType MaxPopulation = new StatType(10, "Max Population", "POP");
        public static readonly StatType MaxFood = new StatType(11, "Max Food", "MX FD");
        public static readonly StatType MaxWood = new StatType(12, "Max Wood", "MX WD");
        public static readonly StatType MaxStone = new StatType(13, "Max Stone", "MX ST");

        public static readonly StatType GroupFoodRatePerPop = new StatType(14, "Food Use Per Turn Per Population","FD/T/P");
        public static readonly StatType GroupFoodRate = new StatType(15, "Food Use Per Turn", "FD/T");
        public static readonly StatType GroupWoodRatePerPop = new StatType(16, "Wood Use In Winter Per Population", "WD/W/P");
        public static readonly StatType GroupWoodRate = new StatType(17, "Wood Use In Winter", "WD/W");
        public static readonly StatType GroupLaborPerPop = new StatType(18, "Labor Per Turn Per Population", "L/T/P");
        public static readonly StatType GroupLaborRate = new StatType(19, "Labor Per Turn", "L/T");

        public static readonly StatType GroupBirthRate = new StatType(20, "Birth Rate", "BR");
        public static readonly StatType GroupDeathRate = new StatType(21, "Death Rate", "DR");
        public static readonly StatType GroupStarvationRate = new StatType(22, "Starvation Rate", "SR");
        public static readonly StatType GroupFreezeRate = new StatType(23, "Freeze Rate", "FR");
        public static readonly StatType StartingGold = new StatType(24, "Starting Gold");


        //Tile Stats
        public static readonly StatType DefenseBonus = new StatType(30, "Defense Bonus", "DB");
        public static readonly StatType MovementCost = new StatType(31, "Movement Cost", "MC");


        //Land Stats
        public static readonly StatType FoodYield = new StatType(40, "Food Yield", "FY");
        public static readonly StatType WoodYield = new StatType(41, "Wood Yield", "WY");
        public static readonly StatType StoneYield = new StatType(42, "Stone Yield", "SY");
        public static readonly StatType GoldYield = new StatType(43, "Gold Yield", "GY");

        // Work Land Stats
        public static readonly StatType FishFoodRate = new StatType(50, "Fishing Rate");
        public static readonly StatType HuntFoodRate = new StatType(51, "Hunting Rate");
        public static readonly StatType FarmFoodRate = new StatType(52, "Farm Rate");
        public static readonly StatType ChopWoodRate = new StatType(53, "Chop Wood Rate");
        public static readonly StatType QuarryStoneRate = new StatType(54, "Quarry Rate");

        public static readonly StatType HerdMaxPopRate = new StatType(60, "Max Herd");

        //Improvement Stats
        public static readonly StatType MaxCondition = new StatType(70, "Max Condition", "MX CD");     


        #endregion


        #region StatType Object

        [SerializeField]
        protected string _abbreviation;
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

