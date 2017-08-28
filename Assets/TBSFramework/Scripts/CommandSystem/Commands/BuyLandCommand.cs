using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;
using System;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]

    public class BuyLandCommand : Command
    {
        public BuyLandCommand()
        {

        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            Cost cost = new Cost(GoodType.Gold, StatType.LandCostModifier, -tile.Stats[StatType.LandValue], "Bought " + tile.Land.Name);
            owner.Goods.ProcessNow(cost.ModifiedTransaction(owner.Stats));
            tile.Owner = owner;
        }
        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            if (tile.Owner != owner && owner.Goods.Gold.Value >= tile.Stats[StatType.LandValue])
                return true;
            else
                return false;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return GameDatabase.Instance.TileGraphics["Icon_16px_LandValue"];

        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            if (tile.Owner == group)
            {
                sb.AppendLine(("Buy " + tile.Land.Name +" for "+ tile.Stats[StatType.LandValue] + GoodType.Gold.Icon).ToHeaderStyle().ToNegativeColor());
                sb.AppendLine(("You already own " + tile.Land.Name).ToNegativeColor());
            }
            else if (group.Goods.Gold.Value < tile.Stats[StatType.LandValue])
            {
                sb.AppendLine(("Buy " + tile.Land.Name + " for " + tile.Stats[StatType.LandValue] + GoodType.Gold.Icon).ToHeaderStyle().ToNegativeColor());
                sb.AppendLine(("You have " + group.Goods[GoodType.Gold] + GoodType.Gold.Icon).ToNegativeColor());

            }
            else
            {
                sb.AppendLine(("Buy " + tile.Land.Name + " for " + tile.Stats[StatType.LandValue] + GoodType.Gold.Icon).ToHeaderStyle().ToPositiveColor());
            }

            return sb.ToString();
        }
    }
}
