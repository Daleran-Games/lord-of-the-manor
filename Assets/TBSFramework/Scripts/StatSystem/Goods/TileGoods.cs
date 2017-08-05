using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class TileGoods : GoodsCollection
    {
        //[System.NonSerialized]
        protected HexTile parentTile;

        protected int condition;
        public virtual Good Condition { get { return new Good(GoodType.Condition, condition); } }

        public TileGoods(HexTile tile) : base()
        {
            parentTile = tile;
            condition = 0;

        }

        public override int this[GoodType type]
        {
            get
            {
                switch (type)
                {
                    case GoodType.Condition:
                        return Condition.Value;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (type)
                {
                    case GoodType.Condition:
                        condition = value;
                        OnGoodChanged(this, Condition.Type);
                        break;
                }
            }
        }

        public override List<Good> Goods
        {
            get
            {
                return new List<Good> { Condition };
            }
        }

        public override bool ContainsGoodOfType(GoodType type)
        {
            if (
                type == GoodType.Condition
                )
                return true;
            else
                return false;
        }

        public override void ResolveEdgeCases()
        {

        }

    }

}
