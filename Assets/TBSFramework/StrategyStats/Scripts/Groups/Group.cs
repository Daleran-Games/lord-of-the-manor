using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Group : IComparable, IComparable<Group>, IEquatable<Group>
    {
        protected string name;
        public string Name { get { return name; } }

        public static readonly Group NullUnit = new NullGroup("NullGroup", GroupType.NullGroupType);

        public Group (string name, GroupType type)
        {
            Initialize(name, type);
        }

        protected virtual void Initialize (string name, GroupType type)
        {
            this.name = name;

            Goods = new GroupGoods(this);
            Modifiers = new ModifierCollection();

            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
            GameManager.Instance.Play.StateEnabled += OnGameStart;

            GroupType = type;

            Goods[GoodType.Food] = MaxFood;
            Goods[GoodType.Wood] = MaxWood;
            Goods[GoodType.Stone] = MaxStone;
            Goods[GoodType.Gold] = GroupType.StartingGold;
            Goods[GoodType.Population] = MaxPopulation;
            Goods[GoodType.Work] = WorkRate;

            Debug.Log("Player goods set");

            if (GameManager.Instance.CurrentState is PlayState)
                OnGameStart(GameManager.Instance.CurrentState);
        }

        #region Interface implementations
        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            else return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Group) && Equals((Group)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (name.GetHashCode()) ^ GroupType.GetHashCode();
            }
        }

        public int CompareTo(Group other)
        {
             return GroupType.Rank.CompareTo(other.GroupType.Rank);
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            if (obj.GetType() == typeof(Group) && Equals((Group)obj))
                return CompareTo((Group)obj);
            else
                throw new ArgumentException("Object is not a Group");
        }
        # endregion

        #region Group Stats
        public IModifierCollection Modifiers;
        public GroupGoods Goods;

        public virtual Stat MaxActionPoints { get { return new Stat(StatType.MaxActionPoints, GroupType.MaxActionPoints + Modifiers[StatType.MaxActionPoints]); } }
        public virtual Stat StrengthPerPop { get { return new Stat(StatType.StrengthPerPop, GroupType.StrengthPerPop + Modifiers[StatType.StrengthPerPop]); } }
        public virtual Stat Strength { get { return new Stat(StatType.Strength, (StrengthPerPop * Goods.Population.Value) + Modifiers[StatType.Strength]); } }
        public virtual Stat AttackCost { get { return new Stat(StatType.AttackCost, GroupType.AttackCost + Modifiers[StatType.AttackCost]); } }

        public virtual Stat MaxFood { get { return new Stat(StatType.MaxFood, GroupType.MaxFood + Modifiers[StatType.MaxFood]); } }
        public virtual Stat MaxWood { get { return new Stat(StatType.MaxWood, GroupType.MaxWood + Modifiers[StatType.MaxWood]); } }
        public virtual Stat MaxStone { get { return new Stat(StatType.MaxStone, GroupType.MaxStone + Modifiers[StatType.MaxStone]); } }
        public virtual Stat MaxPopulation { get { return new Stat(StatType.MaxPopulation, GroupType.MaxPopulation + Modifiers[StatType.MaxPopulation]); } }

        public virtual Stat FoodRatePerPop { get { return new Stat(StatType.GroupFoodRatePerPop, GroupType.FoodRatePerPop + Modifiers[StatType.GroupFoodRatePerPop]); } }
        public virtual Stat FoodRate { get { return new Stat(StatType.GroupFoodRate,(FoodRatePerPop * Goods.Population.Value) + Modifiers[StatType.GroupFoodRate]); } }
        public virtual Stat WoodRatePerPop { get { return new Stat(StatType.GroupWoodRatePerPop, GroupType.WoodRatePerPop + Modifiers[StatType.GroupWoodRatePerPop]); } }
        public virtual Stat WoodRate { get { return new Stat(StatType.GroupWoodRate, (WoodRatePerPop * Goods.Population.Value) + Modifiers[StatType.GroupWoodRate]); } }
        public virtual Stat WorkPerPop { get { return new Stat(StatType.GroupWorkPerPop, GroupType.WorkPerPop + Modifiers[StatType.GroupWorkPerPop]); } }
        public virtual Stat WorkRate { get { return new Stat(StatType.GroupWorkRate, (WorkPerPop * Goods.Population.Value) + Modifiers[StatType.GroupWorkRate]); } }

        public virtual Stat BirthRate { get { return new Stat(StatType.GroupBirthRate, GroupType.BirthRate + Modifiers[StatType.GroupBirthRate]); } }
        public virtual Stat DeathRate { get { return new Stat(StatType.GroupDeathRate, GroupType.DeathRate + Modifiers[StatType.GroupDeathRate]); } }
        public virtual Stat StarvationRate { get { return new Stat(StatType.GroupStarvationRate, GroupType.StarvationRate + Modifiers[StatType.GroupStarvationRate]); } }
        public virtual Stat FreezingRate { get { return new Stat(StatType.GroupFreezeRate, GroupType.FreezingRate + Modifiers[StatType.GroupFreezeRate]); } }


        #endregion


        #region GroupType

        [SerializeField]
        protected GroupType groupType;
        public virtual GroupType GroupType
        {
            get { return groupType; }
            set
            {
                if (groupType != null)
                    groupType.OnDeactivation(this);

                groupType = value;
                groupType.OnActivation(this);
            }
        }

        protected virtual void OnGameStart(GameState state)
        {
            if (GroupType != null)
                GroupType.OnGameStart(this);


        }

        protected virtual void OnTurnEnd(BaseTurn turn)
        {
            GroupType.OnTurnEnd(turn, this);
        }

        protected virtual void OnTurnSetUp(BaseTurn turn)
        {

            GroupType.OnTurnSetUp(turn, this);
        }

        protected virtual void OnTurnStart(BaseTurn turn)
        {
            GroupType.OnTurnStart(turn, this);
            SetUpNextTurn();
        }

        protected virtual void SetUpNextTurn()
        {
            Goods.TryAdd(new Transaction(false, new Good(GoodType.Food, FoodRate),"Food Eaten"));

            if (TurnManager.Instance.CurrentTurn is FallTurn)
                Goods.TryAdd(new Transaction(false, new Good(GoodType.Wood, WoodRate),"Wood for the Winter"));
        }


#endregion

        #region IDisposable Support
        protected bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TurnManager.Instance.TurnEnded -= OnTurnEnd;
                    TurnManager.Instance.TurnSetUp -= OnTurnSetUp;
                    TurnManager.Instance.TurnStart += OnTurnStart;
                    GameManager.Instance.Play.StateEnabled -= OnGameStart;
                    GroupType.OnDeactivation(this);
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
