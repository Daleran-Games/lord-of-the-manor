using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UnitType : IDatabaseObject, IType<Unit>
    {
        [SerializeField]
        protected string name;
        public string Name { get { return name; } }

        [SerializeField]
        protected Ranks rank;
        public Ranks Rank { get { return rank; } }

        [SerializeField]
        protected Cultures culture;
        public Cultures Culture { get { return culture; } }

        [System.NonSerialized]
        protected int id;
        public virtual int ID { get { return id; } }

        [SerializeField]
        [HideInInspector]
        protected string type = "UnitType";
        public string Type { get { return type; } }
        /*
        #region UnitStats
        
        [SerializeField]
        protected int actionPoints = 10;
        public Stat ActionPoints { get { return new Stat(Stat.Category.ActionPoints, actionPoints); } }

        [SerializeField]
        protected int attackPerPop = 2;
        public Stat BaseAttack { get { return new Stat(Stat.Category.AttackPerPop, attackPerPop); } }

        [SerializeField]
        protected int attackCost = 5;
        public Stat AttackCost { get { return new Stat(Stat.Category.AttackCost, attackCost); } }

        [SerializeField]
        protected int defensePerPop = 2;
        public Stat BaseDefense { get { return new Stat(Stat.Category.DefensePerPop, defensePerPop); } }

        [SerializeField]
        protected int foodPerPopPerTurn = 1;
        public Stat FoodUse { get { return new Stat(Stat.Category.FoodPerPopPerTurn, foodPerPopPerTurn); } }

        [SerializeField]
        protected int woodPerPopPerWinter = 1;
        public Stat WinterWood { get { return new Stat(Stat.Category.WoodPerPopPerWinter, woodPerPopPerWinter); } }

        [SerializeField]
        protected int maxFood = 50;
        public Stat MaxFood { get { return new Stat(Stat.Category.MaxFoodStorage, maxFood); } }

        [SerializeField]
        protected int maxWood = 30;
        public Stat MaxWood { get { return new Stat(Stat.Category.MaxFoodStorage, maxWood); } }

        [SerializeField]
        protected int maxStone = 10;
        public Stat MaxStone { get { return new Stat(Stat.Category.MaxFoodStorage, maxStone); } }

        [SerializeField]
        protected int maxPop = 8;
        public Stat MaxPopulation { get { return new Stat(Stat.Category.MaxFoodStorage, maxPop); } }


        public Stat BirthRate { get { return GameplayMetrics.BaseBirthRate; } }
        public Stat DeathRate { get { return GameplayMetrics.BaseDeathRate; } }
        public Stat StarvationRate { get { return GameplayMetrics.BaseStarvationRate; } }
        public Stat FreezingRate { get { return GameplayMetrics.BaseFreezingRate; } }
        #endregion
    */
        public UnitType (UnitType type, int id)
        {
            this.id = id;
            name = type.Name;
            rank = type.Rank;
            culture = type.Culture;
            this.type = this.ToString();
            /*
            actionPoints = type.ActionPoints;
            attackPerPop = type.BaseAttack;
            attackCost = type.AttackCost;
            defensePerPop = type.BaseDefense;
            foodPerPopPerTurn = type.FoodUse;
            woodPerPopPerWinter = type.WinterWood;
            */
        }

        public void OnDatabaseInitialization()
        {

        }

        public virtual void OnActivation(Unit unit)
        {
            if (GameManager.Instance.CurrentState is PlayState)
            {
                OnGameStart(unit);
            }
        }

        public virtual void OnGameStart(Unit unit)
        {

        }

        public virtual void OnTurnEnd(BaseTurn turn, Unit unit)
        {

        }

        public virtual void OnTurnSetUp(BaseTurn turn, Unit unit)
        {

        }

        public virtual void OnTurnStart(BaseTurn turn, Unit unit)
        {

        }

        public virtual void OnDeactivation(Unit unit)
        {

        }

        public string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }

    }
}
