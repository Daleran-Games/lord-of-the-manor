﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class UpgradeActivity : Activity
    {
        public UpgradeActivity(CSVData data, int id)
        {
            this.id = id;
            name = data["name", id];
            workIconName = data["workIcon", id];
            type = data["type", id];
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
