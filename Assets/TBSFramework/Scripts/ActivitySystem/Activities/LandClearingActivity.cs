﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class LandClearingActivity : Activity
    {
        public LandClearingActivity(CSVEntry entry)
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
            //tile.Land.ClearTile(tile);
        }

    }
}
