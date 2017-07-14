using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Group
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
            GroupType = type;

            Goods = new GroupGoods(this);
            Modifiers = new ModifierCollection();

            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
            GameManager.Instance.Play.StateEnabled += OnGameStart;
        }


        #region Group Stats
        public IModifierCollection Modifiers;


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

        public virtual Stat BirthRate { get { return new Stat(StatType.GroupBirthRate, GroupType.BirthRate + Modifiers[StatType.GroupBirthRate]); } }
        public virtual Stat DeathRate { get { return new Stat(StatType.GroupDeathRate, GroupType.DeathRate + Modifiers[StatType.GroupDeathRate]); } }
        public virtual Stat StarvationRate { get { return new Stat(StatType.GroupStarvationRate, GroupType.StarvationRate + Modifiers[StatType.GroupStarvationRate]); } }
        public virtual Stat FreezingRate { get { return new Stat(StatType.GroupFreezeRate, GroupType.FreezingRate + Modifiers[StatType.GroupFreezeRate]); } }


        #endregion

        #region Group Goods
        public GroupGoods Goods;

        # endregion

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
        }

        protected virtual void OnGameStart(GameState state)
        {
            if (GroupType != null)
                GroupType.OnGameStart(this);
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
