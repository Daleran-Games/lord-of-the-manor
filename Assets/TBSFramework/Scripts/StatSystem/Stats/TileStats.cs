using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class TileStats : StatCollection
    {
        protected Group owner;
        public Group Owner
        {
            get { return owner; }
            set
            {
                owner = value;
            }
        }


        public TileStats(Group owner) : base()
        {
            this.owner = owner;
        }

        public override int this[StatType statType]
        {
            get
            {
                if (totals.ContainsKey(statType) || owner.TileModifiers.Contains(statType))
                    return totals[statType] + owner.TileModifiers[statType];
                else
                    return 0;
            }
        }

        public override List<StatType> Types
        {
            get
            {
                List<StatType> types = new List<StatType>(base.Types);
                types.AddRange(owner.TileModifiers.Types);
                return types;
            }
        }

        public override List<Modifier> GetAllOfType(StatType statType)
        {
            if (modifiers.ContainsKey(statType) || owner.TileModifiers.Contains(statType))
            {
                List<Modifier> mods = new List<Modifier>(modifiers[statType]);
                mods.AddRange(owner.TileModifiers.GetAllOfType(statType));
                return mods;
            }
            else
                return new List<Modifier>(0);
        }

        public override List<Modifier> GetAll()
        {
            List<Modifier> mods = new List<Modifier>();
            foreach (KeyValuePair<StatType, List<Modifier>> m in modifiers)
            {
                mods.AddRange(m.Value);
            }
            mods.AddRange(owner.TileModifiers.GetAll());

            return mods;
        }

    }
}
