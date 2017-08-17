using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class BuyCommand : Command
    {
        GoodType good;

        public BuyCommand(GoodType good) : base()
        {
            this.good = good;
        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            return Market.Instance.CanBuy(good, owner);
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            Market.Instance.Buy(good, owner);
        }

    }
}
