using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UpgradeActivity : Activity
    {
        public UpgradeActivity(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            workIconName = entry["workIcon"];
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void DoActivityOnTile(HexTile tile)
        {

        }

        public override bool IsActivityValid(HexTile tile)
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
            return TileGraphic.clear;
        }


    }
}
