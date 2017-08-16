using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

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
            return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_BuyTile"];

        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

    }
}
