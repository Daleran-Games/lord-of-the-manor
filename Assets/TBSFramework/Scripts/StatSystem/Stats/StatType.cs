using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;


namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class StatType : Enumeration
    {
        #region StatTypes

        public static readonly StatType NullStat = new StatType(-1, "Null");


        //Unit Stats
        public static readonly StatType Strength = new StatType(0, "Strength", "Icon_16px_CombatPower".ToSprite(),"Strength is the relative combat power of a unit.");
        public static readonly StatType StrengthPerPop = new StatType(1, "Strength Per Population");
        public static readonly StatType MaxActionPoints = new StatType(2, "Max Action Points", ("Icon_16px_Good_AP".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters());
        public static readonly StatType Happiness = new StatType(3, "Happiness", "Icon_16px_Happiness".ToSprite());
        public static readonly StatType AttackCost = new StatType(4, "Attack Cost", ("Icon_16px_CombatPower".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType GroupLevy = new StatType(5, "Levy Percent");

        public static readonly StatType MaxPopulation = new StatType(10, "Max Population", ("Icon_16px_Good_Population".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters());
        public static readonly StatType MaxFood = new StatType(11, "Max Food", ("Icon_16px_Good_Food".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters());
        public static readonly StatType MaxWood = new StatType(12, "Max Wood", ("Icon_16px_Good_Wood".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters());
        public static readonly StatType MaxStone = new StatType(13, "Max Stone", ("Icon_16px_Good_Stone".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters());

        public static readonly StatType GroupFoodRatePerPop = new StatType(14, "Food Use Per Turn Per Population", ("Icon_16px_Good_Food".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters() + "/" + "Icon_16px_Good_Population".ToSprite());
        public static readonly StatType GroupFoodRate = new StatType(15, "Food Use Per Turn",("Icon_16px_Good_Food".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType GroupWoodRate = new StatType(17, "Wood Use In Winter", ("Icon_16px_Good_Wood".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters() +"/"+ "Icon_16px_Winter".ToSprite());
        public static readonly StatType GroupLaborPerPop = new StatType(18, "Labor Per Turn Per Population", ("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters() + "/" + "Icon_16px_Good_Population".ToSprite());
        public static readonly StatType GroupLaborRate = new StatType(19, "Labor Per Turn", ("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());

        public static readonly StatType GroupBirthRate = new StatType(20, "Birth Rate", ("Icon_16px_Good_Population".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType GroupDeathRate = new StatType(21, "Death Rate", ("Icon_16px_Good_Population".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType GroupStarvationRate = new StatType(22, "Starvation Rate");
        public static readonly StatType GroupFreezeRate = new StatType(23, "Freeze Rate");
        public static readonly StatType StartingGold = new StatType(24, "Starting Gold");
        public static readonly StatType LandCostModifier = new StatType(25, "Land Cost Modifier", "Icon_16px_LandValue".ToSprite());


        //Tile Stats
        public static readonly StatType DefenseBonus = new StatType(30, "Defense Bonus", "Icon_16px_TileDefense".ToSprite());
        public static readonly StatType MovementCost = new StatType(31, "Movement Cost", "Icon_16px_Movement".ToSprite());
        public static readonly StatType LandValue = new StatType(33, "Land Value", "Icon_16px_LandValue".ToSprite());


        //Land Stats
        public static readonly StatType FoodYield = new StatType(40, "Food Yield", ("Icon_16px_Good_Food".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType WoodYield = new StatType(41, "Wood Yield", ("Icon_16px_Good_Wood".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType StoneYield = new StatType(42, "Stone Yield", ("Icon_16px_Good_Stone".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType GoldYield = new StatType(43, "Gold Yield", ("Icon_16px_Good_Gold".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());

        //Feature Stats
        public static readonly StatType MaxCondition = new StatType(50, "Max Condition", ("Icon_16px_Good_Condition".ToSprite() + "Modifier_16px_Max".ToSprite()).OverlapCharacters());

        //Building Stats
        public static readonly StatType BuildLaborCost = new StatType(200, "Build Labor Cost", ("Icon_16px_Good_Labor".ToSprite()+"Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType BuildWoodCost = new StatType(201, "Build Wood Cost", ("Icon_16px_Good_Wood".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType BuildStoneCost = new StatType(202, "Build Stone Cost", ("Icon_16px_Good_Stone".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType BuildGoldCost = new StatType(203, "Build Gold Cost", ("Icon_16px_Good_Gold".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType BuildTime = new StatType(204, "Build Time",("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Turns".ToSprite()).OverlapCharacters());

        //Razing Stats
        // Raze Module Stats
        public static readonly StatType RazeLaborCost = new StatType(210, "Raze Labor Cost", ("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType RazeTime = new StatType(211, "Raze Time", ("Icon_16px_Raze".ToSprite() + "Modifier_16px_Turns".ToSprite()).OverlapCharacters());

        //Farming Stats
        public static readonly StatType FarmLaborCost = new StatType(220, "Seasonal Farm Labor", ("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType FarmGrowTime = new StatType(221, "Farm Grow Time", ("Icon_16px_Farm".ToSprite() + "Modifier_16px_Turns".ToSprite()).OverlapCharacters());
        public static readonly StatType FarmCycleTime = new StatType(222, "Farm Cycle Time", ("Icon_16px_Farm".ToSprite() + "Modifier_16px_Turns".ToSprite()).OverlapCharacters());
        public static readonly StatType FarmingFoodPerYield = new StatType(223, "Farming Good Per Yield", ("Icon_16px_Good_Food".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType FarmingFoodRate = new StatType(224, "Farming Food Per Turn", ("Icon_16px_Good_Food".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());


        //Logging Stats
        public static readonly StatType LoggingLaborCost = new StatType(230, "Seasonal Logging Labor", ("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType LoggingTime = new StatType(231, "Time Untill Forest Cleared", ("Icon_16px_ClearForest".ToSprite() + "Modifier_16px_Turns".ToSprite()).OverlapCharacters());
        public static readonly StatType LoggingWoodPerYield = new StatType(232, "Logging Wood Per Yield", ("Icon_16px_Good_Wood".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType LoggingWoodRate = new StatType(233, "Logging Wood Per Turn", ("Icon_16px_Good_Wood".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());

        //Quarrying Stats
        public static readonly StatType QuarryLaborCost = new StatType(240, "Seasonal Quarry Labor", ("Icon_16px_Good_Labor".ToSprite() + "Modifier_16px_Minus".ToSprite()).OverlapCharacters());
        public static readonly StatType QuarryTime = new StatType(241, "Time untill Quarry Depeleted", ("Icon_16px_Quarry".ToSprite() + "Modifier_16px_Turns".ToSprite()).OverlapCharacters());
        public static readonly StatType QuarryingStonePerYield = new StatType(242, "Quarry Stone Per Yield", ("Icon_16px_Good_Stone".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());
        public static readonly StatType QuarryingStoneRate = new StatType(243, "Quarry Stone Per Turn", ("Icon_16px_Good_Stone".ToSprite() + "Modifier_16px_Plus".ToSprite()).OverlapCharacters());

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

        public StatType () { }

        protected StatType(int value, string displayName) : base(value, displayName)
        {
            _statIcon = "Icon_16_Gen_Question".ToSprite();
            _description = "";
        }

        protected StatType(int value, string displayName, string statIcon) : base(value, displayName)
        {
            _statIcon = statIcon;
            _description = "";
        }

        protected StatType (int value, string displayName, string statIcon, string description) : base (value,displayName)
        {
            _statIcon = statIcon;
            _description = description;
        }

# endregion

    }
}

