using System;
using UnityEngine;
using System.Collections.Generic;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Group : IComparable, IComparable<Group>, IEquatable<Group>
    {
        protected string name;
        public string Name { get { return name; } }

        protected bool home;
        public bool Home {
            get { return home; }
            set
            {
                home = value;

                if (HomeChanged != null)
                HomeChanged(home);
            }
        }
        public event Action<bool> HomeChanged;

        public static readonly Group Null = new NullGroup("NullGroup", GroupType.NullGroupType);
        WorkUtilities workUtils;

        public Group (string name, GroupType type)
        {
            this.name = name;
            Home = false;
            OwnedTiles = new List<HexTile>();
            Goods = new GroupGoods(this);
            Stats = new GroupStats(this);
            TileModifiers = new StatCollection();
            //workUtils = new WorkUtilities();

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

        [System.NonSerialized]
        [HideInInspector]
        public GroupGoods Goods;

        [NonSerialized]
        [HideInInspector]
        public List<HexTile> OwnedTiles;
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

           
            Goods.ProcessNow(new Transaction(GoodType.Food, Stats[StatType.MaxFood], "Starting Food"));
            
            Goods.ProcessNow(new Transaction(GoodType.Wood, Stats[StatType.MaxWood], "Starting Wood"));
            
            Goods.ProcessNow(new Transaction(GoodType.Stone, Stats[StatType.MaxStone], "Starting Stone"));
            
            Goods.ProcessNow(new Transaction(GoodType.Gold, Stats[StatType.StartingGold], "Starting Gold"));

            Goods.ProcessNow(new Transaction(GoodType.Population, Stats[StatType.MaxPopulation], "Starting Population"));
            Goods.ProcessNow(new Transaction(GoodType.Labor, Stats[StatType.GroupLaborRate], "Labor from your Clan"));
            Goods.AddFuture(new Transaction(GoodType.Labor, Stats[StatType.GroupLaborRate], "Labor from your Clan"));
        }

        public virtual void OnTurnEnd(BaseTurn turn)
        {
            //Debug.Log("This got called");
            GroupType.OnTurnEnd(turn, this);
            Goods.ResetWork();
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
            //workUtils.OptimizeLabor(this);
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
