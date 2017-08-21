using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class GoodType : Enumeration
    {

        #region StatTypes
        public static GoodType None = new GoodType(-1,"None", "Icon_16_Gen_Question".ToSprite(),"This is a null stat. You shouldn't see this.");
        public static GoodType Food = new GoodType(0, "Food", "Icon_16px_Good_Food".ToSprite(), "Each clan member requires one food every turn to live. If they do not recieve any food, there is a 50% chance they will die.");
        public static GoodType Wood = new GoodType(1, "Wood", "Icon_16px_Good_Wood".ToSprite(), "Wood is used in construction and heating dwellings in the winter. Larger dwellings require more wood. If a dwelling does not recieve enough wood, there is a 25% chance clan members could die.");
        public static GoodType Stone = new GoodType(2, "Stone", "Icon_16px_Good_Stone".ToSprite(), "Stone is used in advanced construction projects. It is expensive and requires a lot of labor to quarry and move.");
        public static GoodType Gold = new GoodType(3, "Gold", "Icon_16px_Good_Gold".ToSprite(), "Gold is used for buying land and trading goods on the market.");
        public static GoodType Labor = new GoodType(4, "Labor", "Icon_16px_Good_Labor".ToSprite(), "Labor represents the amount work you can reasonably do per season. An average laborer produces 6 labor a season, one for each day of the week excluding Sunday.");
        public static GoodType Population = new GoodType(5, "Population", "Icon_16px_Good_Population".ToSprite(), "Your clan's populations is limited by the size of their dwelling.");
        public static GoodType ActionPoints = new GoodType(6, "Action Points", "Icon_16px_Good_AP".ToSprite(), "Action Points.");
        public static GoodType Condition = new GoodType(7, "Condition", "Icon_16px_Good_Condition".ToSprite(), "Condition.");
        public static GoodType Turns = new GoodType(8, "Turns", "Icon_16px_Good_Turns".ToSprite(), "Turns.");

        #endregion

        #region StatType Object

        [SerializeField]
        protected string _goodIcon;
        public string Icon { get { return _goodIcon; } }

        [SerializeField]
        protected string _description;
        public string Description { get { return _description; } }


        public GoodType() { }

        protected GoodType(int value, string displayName, string statIcon, string description) : base (value,displayName)
        {
            _goodIcon = statIcon;
            _description = description;
        }

        #endregion
    }
}
