using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandClearingActivity : Activity
    {
        public LandClearingActivity(string[] csv)
        {
            id = Int32.Parse(csv[0]);
            name = csv[1];
            type = csv[2];
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            if (tile.Land.Clearable(tile))
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
            return tile.Land.Clearable(tile);
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            //tile.Land.ClearTile(tile);
        }

    }
}
