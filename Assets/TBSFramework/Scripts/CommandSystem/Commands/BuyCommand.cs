using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;
using System;

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

        public override string GetInfo(HexTile tile, Group group)
        {
            return "Not Valid For a Tile, Something went wrong".ToNegativeColor();
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
