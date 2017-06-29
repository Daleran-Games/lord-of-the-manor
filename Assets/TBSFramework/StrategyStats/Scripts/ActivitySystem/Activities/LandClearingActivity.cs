using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewLandClearingActivity", menuName = "DaleranGames/TBS/Activities/Clear Land", order = 0)]
    public class LandClearingActivity : Activity
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            if (tile.Land.Clearable)
                return tile.Land.ClearedLand.IconGraphic;
            else
                return TileGraphic.clear;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override bool IsActivityValid(HexTile tile)
        {
            return tile.Land.Clearable;
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Land.ClearTile(tile);
        }
    }
}
