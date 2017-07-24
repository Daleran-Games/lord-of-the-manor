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

        }

        public GroupType(CSVData data, int id)
        {
            this.id = id;
            name = data["name", id];
            type = data["type", id];
            rank = (Ranks)Enum.Parse(typeof(Ranks), data["rank",id]);
            culture = (Cultures)Enum.Parse(typeof(Cultures), data["culture", id]);



            tileModifiers = Modifier.ParseCSVList(data.ParseList("tileModifierList", id));
            groupModifiers = Modifier.ParseCSVList(data.ParseList("groupModifierList", id));

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
        protected Modifier[] groupModifiers;
        public virtual Modifier[] GroupModifiers { get { return groupModifiers; } }

        [SerializeField]
        protected Modifier[] tileModifiers;
        public virtual Modifier[] TileModifiers { get { return tileModifiers; } }
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

            if (GroupModifiers != null)
                group.Stats.Add(GroupModifiers);
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
            if (GroupModifiers != null)
                group.Stats.Remove(GroupModifiers);
        }
        #endregion



    }
}
