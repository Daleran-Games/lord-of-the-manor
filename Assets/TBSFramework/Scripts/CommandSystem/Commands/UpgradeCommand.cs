using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DaleranGames.UI;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UpgradeCommand : Command
    {
        public UpgradeCommand()
        {

        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            IUpgradeable upgradeable = tile.Feature as IUpgradeable;
            if (upgradeable != null)
            {
                if (upgradeable.CanUpgrade(tile) && owner == tile.Owner)
                    return true;
            }
            return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            IUpgradeable upgradeable = tile.Feature as IUpgradeable;
            upgradeable.Upgrade(tile);
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {

            IUpgradeable upgradeable = tile.Feature as IUpgradeable;
            if (upgradeable != null)
                return upgradeable.GetUpgradeGraphic(tile);
            else
                return TileGraphic.Clear;
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            IUpgradeable upgradeable = tile.Feature as IUpgradeable;
            if (upgradeable == null)
            {
                return (("Tile Not Upgradeable").ToNegativeColor());
            } else
            {
                if (!upgradeable.CanUpgrade(tile))
                    return ("Cannot afford to upgrade " + tile.Feature.Name).ToNegativeColor();

                if (group != tile.Owner)
                    return ("You do not own " + tile.Feature.Name).ToNegativeColor();

                return ("Upgrade " + tile.Feature.Name).ToPositiveColor().ToHeaderStyle();
            }
        }
    }
}
