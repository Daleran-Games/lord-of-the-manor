using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewUpgradeActivity", menuName = "DaleranGames/TBS/Activities/Upgrade", order = 0)]
    public class UpgradeActivity : Activity
    {

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

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

    }
}
