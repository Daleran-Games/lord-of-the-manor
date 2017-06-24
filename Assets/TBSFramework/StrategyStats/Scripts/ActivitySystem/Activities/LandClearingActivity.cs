using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewLandClearingActivity", menuName = "DaleranGames/TBS/Activities/Clear Land", order = 0)]
    public class LandClearingActivity : Activity
    {
        public override Vector2Int GetTerrainIcon(HexTile tile)
        {
            if (tile.Land.CanClear())
                return tile.Land.ClearedLand.LandIcon;
            else
                return Vector2Int.zero;
        }

        public override Vector2Int GetUIIcon(HexTile tile)
        {
            return Vector2Int.zero;
        }

        public override bool IsActivityValid(HexTile tile)
        {
            return tile.Land.CanClear();
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Land.ClearTile(tile);
        }
    }
}
