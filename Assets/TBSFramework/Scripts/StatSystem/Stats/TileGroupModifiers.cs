using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TileGroupModifiers : StatCollection
    {
        protected Group owner;
        public Group Owner
        {
            get { return owner; }
            set
            {
                owner.Stats.Remove(GetAll());
                owner = value;
                owner.Stats.Add(GetAll());
            }
        }

        public TileGroupModifiers(Group owner) : base()
        {
            this.owner = owner;
        }

        public override void Add(Modifier mod)
        {
            base.Add(mod);
            owner.Stats.Add(mod);
        }

        public override void Add(IList<Modifier> mods)
        {
            base.Add(mods);
        }

        public override void Remove(Modifier mod)
        {
            base.Remove(mod);
            owner.Stats.Remove(mod);
        }

        public override void Remove(IList<Modifier> mods)
        {
            base.Remove(mods);
        }

        public override void Clear(StatType statType)
        {
            owner.Stats.Remove(this.GetAllOfType(statType));
            base.Clear(statType);
        }

        public override void ClearAll()
        {
            owner.Stats.Remove(this.GetAll());
            base.ClearAll();
        }


    }
}
