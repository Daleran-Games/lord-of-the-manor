using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class GroupType : IDatabaseObject, IType<Group>
    {

        //Private constructor for a null type group
        private GroupType(string name)
        {
            id = -1;
            this.name = name;
            rank = Ranks.None;
            culture = Cultures.None;
            this.type = this.ToString();

            maxActionPoints = 0;
            strengthPerPop = 0;
            attackCost = 0;
            foodPerPopPerTurn = 0;
            woodPerPopPerWinter = 0;
            maxFood = 0;
            maxWood = 0;
            maxStone = 0;
            maxPopulation = 0;
        }

        public GroupType(string[] csvLine)
        {
            id = Int32.Parse(csvLine[0]);
            name = csvLine[1];
            type = csvLine[2];
            rank = (Ranks)Enum.Parse(typeof(Ranks), csvLine[3]);
            culture = (Cultures)Enum.Parse(typeof(Cultures), csvLine[4]);

            maxActionPoints = Int32.Parse(csvLine[5]);
            strengthPerPop = Int32.Parse(csvLine[6]);
            levyPercent = Int32.Parse(csvLine[7]);
            attackCost = Int32.Parse(csvLine[8]);
            foodPerPopPerTurn = Int32.Parse(csvLine[9]);
            woodPerPopPerWinter = Int32.Parse(csvLine[10]);
            workPerPop = Int32.Parse(csvLine[11]);
            maxFood = Int32.Parse(csvLine[12]);
            maxWood = Int32.Parse(csvLine[13]);
            maxStone = Int32.Parse(csvLine[14]);
            startingGold = Int32.Parse(csvLine[15]);
            maxPopulation = Int32.Parse(csvLine[16]);

            groupTypeModifiers = Modifier.ParseCSVList(CSVUtility.ParseList(csvLine,"groupModifiers"));

        }

        [Header("Group Info")]
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [SerializeField]
        protected int id;
        public virtual int ID { get { return id; } }

        [SerializeField]
        [HideInInspector]
        protected string type = "GroupType";
        public virtual string Type { get { return type; } }

        [SerializeField]
        protected Ranks rank;
        public virtual Ranks Rank { get { return rank; } }

        [SerializeField]
        protected Cultures culture;
        public Cultures Culture { get { return culture; } }

        public static readonly GroupType NullGroupType = new GroupType("NullGroup");

        #region UnitStats

        [Header("Combat Stats")]
        [SerializeField]
        protected int maxActionPoints = 10;
        public virtual Stat MaxActionPoints { get { return new Stat(StatType.MaxActionPoints, maxActionPoints); } }

        [SerializeField]
        protected int strengthPerPop = 2;
        public virtual Stat StrengthPerPop { get { return new Stat(StatType.StrengthPerPop, strengthPerPop); } }

        [SerializeField]
        protected int attackCost = -5;
        public virtual Stat AttackCost { get { return new Stat(StatType.AttackCost, attackCost); } }

        [SerializeField]
        protected int levyPercent = 0;
        public virtual Stat LevyPervent { get { return new Stat(StatType.GroupLevy, levyPercent); } }

        [SerializeField]
        protected int foodPerPopPerTurn = -1;
        public virtual Stat FoodRatePerPop { get { return new Stat(StatType.GroupFoodRatePerPop, foodPerPopPerTurn); } }

        [SerializeField]
        protected int woodPerPopPerWinter = -1;
        public virtual Stat WoodRatePerPop { get { return new Stat(StatType.GroupWoodRatePerPop, woodPerPopPerWinter); } }

        [SerializeField]
        protected int workPerPop = 12;
        public virtual Stat WorkPerPop { get { return new Stat(StatType.GroupWorkPerPop, workPerPop); } }

        [SerializeField]
        protected int maxFood = 50;
        public virtual Stat MaxFood { get { return new Stat(StatType.MaxFood, maxFood); } }

        [SerializeField]
        protected int maxWood = 30;
        public virtual Stat MaxWood { get { return new Stat(StatType.MaxWood, maxWood); } }

        [SerializeField]
        protected int maxStone = 10;
        public virtual Stat MaxStone { get { return new Stat(StatType.MaxStone, maxStone); } }

        [SerializeField]
        protected int startingGold = 150;
        public int StartingGold { get { return startingGold; } }

        [SerializeField]
        protected int maxPopulation = 8;
        public virtual Stat MaxPopulation { get { return new Stat(StatType.MaxPopulation, maxPopulation); } }

        public Stat BirthRate { get { return GameplayMetrics.BaseBirthRateStat; } }
        public Stat DeathRate { get { return GameplayMetrics.BaseDeathRateStats; } }
        public Stat StarvationRate { get { return GameplayMetrics.BaseStarvationRateStats; } }
        public Stat FreezingRate { get { return GameplayMetrics.BaseFreezingRateStats; } }


        //Turn all stats into read only ints that get turned into the modifers and added to the group at runtime;
        [SerializeField]
        protected Modifier[] groupTypeModifiers;
        public virtual Modifier[] GroupTypeModifiers { get { return groupTypeModifiers; } }
        #endregion

        # region Group Callbacks
        public virtual void OnDatabaseInitialization()
        {

        }

        public virtual void OnActivation(Group group)
        {
            if (GameManager.Instance.CurrentState is PlayState)
            {
                OnGameStart(group);
            }

            if (GroupTypeModifiers != null)
                group.Modifiers.Add(GroupTypeModifiers);
        }

        public virtual void OnGameStart(Group group)
        {

        }

        public virtual void OnTurnEnd(BaseTurn turn, Group group)
        {

        }

        public virtual void OnTurnSetUp(BaseTurn turn, Group group)
        {

        }

        public virtual void OnTurnStart(BaseTurn turn, Group group)
        {

        }

        public virtual void OnDeactivation(Group group)
        {
            if (GroupTypeModifiers != null)
                group.Modifiers.Remove(GroupTypeModifiers);
        }
        #endregion



    }
}
