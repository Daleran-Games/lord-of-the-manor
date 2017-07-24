using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class NullGroup : Group
    {
        public NullGroup(string name, GroupType type) : base(name, type)
        {

        }

        protected override void Initialize(string name, GroupType type)
        {
            this.name = name;
            GroupType = GroupType.NullGroupType;

            Goods = new NullUnitGoods(this);
            Stats = new NullStatCollection();
        }

        public override GroupType GroupType
        {
            get { return groupType; }
            set
            {

            }
        }



        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

    }
}
