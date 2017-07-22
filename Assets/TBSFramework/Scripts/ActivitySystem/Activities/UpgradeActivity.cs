using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UpgradeActivity : Activity
    {
        public UpgradeActivity(string[] csv)
        {
            id = System.Int32.Parse(csv[0]);
            name = csv[1];
            workIconName = csv[2];
            type = csv[3];
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            if (tile.Improvement != null)
                tile.Improvement.Upgrade(tile);
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
