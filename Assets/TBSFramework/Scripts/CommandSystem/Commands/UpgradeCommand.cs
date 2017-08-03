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
            activity = CommandType.Upgrade;
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
            if (tile.Improvement != null)
                return tile.Improvement.Upgradeable;

            return false;
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            if (tile.Improvement !=null)
            {
                if (tile.Improvement.Upgradeable)
                    return tile.Improvement.UpgradedImprovement.IconGraphic;
            }
            return TileGraphic.Clear;
        }


    }
}
