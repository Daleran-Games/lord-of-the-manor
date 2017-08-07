using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        public override TileGraphic GetUIIcon(HexTile tile)
        {

            IUpgradeable upgradeable = tile.Feature as IUpgradeable;
            if (upgradeable != null)
                return upgradeable.GetUpgradeGraphic(tile);
            else
                return TileGraphic.Clear;
        }
    }
}
