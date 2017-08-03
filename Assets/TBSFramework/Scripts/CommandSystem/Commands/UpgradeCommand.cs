using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UpgradeCommand : Command
    {
        public UpgradeCommand(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            commandIconName = entry["workIcon"];
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void PreformCommand(HexTile tile)
        {

        }

        public override bool IsValidCommand(HexTile tile)
        {
            if (tile.Feature != null)
                return tile.Feature.Upgradeable;

            return false;
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            if (tile.Feature !=null)
            {
                if (tile.Feature.Upgradeable)
                    return tile.Feature.UpgradedImprovement.IconGraphic;
            }
            return TileGraphic.Clear;
        }


    }
}
