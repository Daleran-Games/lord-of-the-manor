using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewUpgradeActivity", menuName = "DaleranGames/TBS/Activities/Upgrade", order = 0)]
    public class UpgradeActivity : Activity
    {

        public override void DoActivityOnTile(HexTile tile)
        {
            if (tile.Improvement != null)
                tile.Improvement.Upgrade(tile);
        }

        public override bool IsActivityValid(HexTile tile)
        {
            if (tile.Improvement != null)
                return tile.Improvement.CheckIfCanUpgrade();

            return false;
        }

        public override Vector2Int GetTerrainIcon(HexTile tile)
        {
            if (tile.Improvement !=null)
            {
                if (tile.Improvement.CheckIfCanUpgrade())
                    return tile.Improvement.UpgradedImprovement.IconGraphic;
            }
            return Vector2Int.zero;
        }

        public override Vector2Int GetUIIcon(HexTile tile)
        {
            return Vector2Int.zero;
        }

    }
}
