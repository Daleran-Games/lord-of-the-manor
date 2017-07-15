using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class TileGoods : GoodsCollection
    {
        [System.NonSerialized]
        protected HexTile parentTile;

        protected int condition;
        public virtual Good Condition { get { return new Good(GoodType.Condition, condition); } }

        public TileGoods(HexTile tile) : base()
        {
            parentTile = tile;
        }

        public override Good this[GoodType type]
        {
            get
            {
                switch (type)
                {
                    case GoodType.Condition:
                        return Condition;
                    default:
                        return null;
                }
            }
            set
            {
                switch (type)
                {
                    case GoodType.Condition:
                        condition = value.Value;
                        break;
                }
            }
        }

        public override Good[] GetAllGoods()
        {
            return new Good[]
            {
                Condition
            };
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
