using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class NullStatCollection : StatCollection
    {
        public NullStatCollection() : base()
        {

        }

        public override List<StatType> Types
        {
            get
            {
                return new List<StatType>(0);
            }
        }

        public override int this[StatType statType]
        {
            get
            {
                return 0;
            }
        }

        public override void Add(Modifier mod)
        {

        }

        public override void Add(IList<Modifier> mods)
        {

        }

        public override void Clear(StatType statType)
        {

        }

        public override void ClearAll()
        {

        }

        public override void Remove(Modifier mod)
        {
            
        }

        public override void Remove(IList<Modifier> mods)
        {

        }

        public override List<Modifier> GetAllOfType(StatType statType)
        {
            return new List<Modifier>(0);
        }

        public override List<Modifier> GetAll()
        {
            return new List<Modifier>(0);
        }

        public override bool Contains(StatType statType)
        {
            return false;
        }

        protected override void RaiseStatModified(IStatCollection<StatType> stats, StatType statType)
        {
            
        }
    }
}

