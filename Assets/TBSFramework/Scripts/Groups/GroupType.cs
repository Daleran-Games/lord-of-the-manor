using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class GroupType : IDatabaseObject, IType<Group>
    {
        public static readonly GroupType Null = new GroupType("Null Group Type");

        //Private constructor for a null type group
        private GroupType(string name)
        {
            id = -1;
            this.name = name;
            rank = Ranks.None;
            culture = Cultures.None;
            this.type = "GroupType";

            tileModifiers = new List<Modifier>(0);
            groupModifiers = new List<Modifier>(0);

        }

        public GroupType(CSVEntry entry)
        {

            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            rank = (Ranks)Enum.Parse(typeof(Ranks), entry["rank"]);
            culture = (Cultures)Enum.Parse(typeof(Cultures), entry["culture"]);

            tileModifiers = Modifier.ParseCSVList(entry.ParseList("tileModifiers"));
            groupModifiers = Modifier.ParseCSVList(entry.ParseList("groupModifiers"));

            groupModifiers.Add(new Modifier(StatType.GroupFoodRatePerPop, Int32.Parse(entry["foodPerTurn"]), name));
            groupModifiers.Add(new Modifier(StatType.GroupWoodRatePerPop, Int32.Parse(entry["woodPerWinter"]), name));
            groupModifiers.Add(new Modifier(StatType.GroupLaborPerPop, Int32.Parse(entry["laborPerPop"]), name));
            groupModifiers.Add(new Modifier(StatType.MaxFood, Int32.Parse(entry["maxFood"]), name));
            groupModifiers.Add(new Modifier(StatType.MaxWood, Int32.Parse(entry["maxWood"]), name));
            groupModifiers.Add(new Modifier(StatType.MaxStone, Int32.Parse(entry["maxStone"]), name));
            groupModifiers.Add(new Modifier(StatType.StartingGold, Int32.Parse(entry["startingGold"]), name));
            groupModifiers.Add(new Modifier(StatType.MaxPopulation, Int32.Parse(entry["maxPopulation"]), name));

            groupModifiers.Add(new Modifier(StatType.GroupBirthRate,GameplayMetrics.DefaultBirthRate,name));
            groupModifiers.Add(new Modifier(StatType.GroupDeathRate, GameplayMetrics.DefaultDeathRate, name));
            groupModifiers.Add(new Modifier(StatType.GroupStarvationRate, GameplayMetrics.DefaultStarvationRate, name));
            groupModifiers.Add(new Modifier(StatType.GroupFreezeRate, GameplayMetrics.DefaultFreezingRate, name));
        }

        [Header("Group Info")]
        [SerializeField]
        protected string name;
        public virtual string Name { get { return name; } }

        [System.NonSerialized]
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
        [SerializeField]
        protected List<Modifier> groupModifiers;
        public virtual ReadOnlyCollection<Modifier> GroupModifiers { get { return groupModifiers.AsReadOnly(); } }

        [SerializeField]
        protected List<Modifier> tileModifiers;
        public virtual ReadOnlyCollection<Modifier> TileModifiers { get { return tileModifiers.AsReadOnly(); } }
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
            group.Stats.Add(groupModifiers);
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
            group.Stats.Remove(groupModifiers);
        }
        #endregion



    }
}
