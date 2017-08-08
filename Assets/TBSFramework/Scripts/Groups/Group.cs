using System;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Group : IComparable, IComparable<Group>, IEquatable<Group>
    {
        protected string name;
        public string Name { get { return name; } }

        public static readonly Group Null = new NullGroup("NullGroup", GroupType.NullGroupType);

        public Group (string name, GroupType type)
        {
            this.name = name;

            Goods = new GroupGoods(this);
            Stats = new GroupStats(this);
            TileModifiers = new StatCollection();

            GroupType = type;
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
        #endregion

        #region Group Stats

        public GroupStats Stats;
        public StatCollection TileModifiers;
        public GroupGoods Goods;
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

                if (value == null)
                    groupType = GroupType.Null;
                else
                    groupType = value;

                groupType.OnActivation(this);
            }
        }

       public virtual void OnGameStart(GameState state)
        {
            GroupType.OnGameStart(this);

            Goods[GoodType.Food] = Stats[StatType.MaxFood];
            Goods[GoodType.Wood] = Stats[StatType.MaxWood];
            Goods[GoodType.Stone] = Stats[StatType.MaxStone];
            Goods[GoodType.Gold] = Stats[StatType.StartingGold];
            Goods[GoodType.Population] = Stats[StatType.MaxPopulation];
            Goods[GoodType.Labor] = Stats[StatType.GroupLaborRate];
            Goods.AddFuture(new Transaction(GoodType.Labor, Stats[StatType.GroupLaborRate], "Work"));
        }

        public virtual void OnTurnEnd(BaseTurn turn)
        {
            //Debug.Log("This got called");
            GroupType.OnTurnEnd(turn, this);
            Goods[GoodType.Labor] = 0;
            Goods.ProcessAllPendingTransactions();
            Goods.ResolveEdgeCases();
        }

        public virtual void OnTurnSetUp(BaseTurn turn)
        {
            GroupType.OnTurnSetUp(turn, this);
            SetUpNextTurn();
        }

        public virtual void OnTurnStart(BaseTurn turn)
        {
            GroupType.OnTurnStart(turn, this);
        }

        protected virtual void SetUpNextTurn()
        {
            Goods.AddFuture(new Transaction(GoodType.Food, Stats[StatType.GroupFoodRate], "Food Eaten"));

            if (TurnManager.Instance.CurrentTurn is FallTurn)
                Goods.AddFuture(new Transaction(GoodType.Wood, Stats[StatType.GroupWoodRate], "Firewood for the Winter"));
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


        #region Save and Loading Support
        public class GroupSaveData
        {
            public readonly int GroupTypeID;
            public readonly int Food;
            public readonly int Wood;
            public readonly int Stone;
            public readonly int Gold;
            public readonly int Population;
            public readonly int Work;

            public GroupSaveData(Group group)
            {


            }
        }
        #endregion
    }
}
