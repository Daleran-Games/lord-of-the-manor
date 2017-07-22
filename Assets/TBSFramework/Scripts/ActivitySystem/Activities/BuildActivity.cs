﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class BuildActivity : Activity
    {
        [SerializeField]
        string improvementName;

        [SerializeField]
        ImprovementType improvement;

        public BuildActivity(string[] csv)
        {
            id = Int32.Parse(csv[0]);
            name = csv[1];
            workIconName = csv[2];
            type = csv[3];
            improvementName = csv[4];
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnDatabaseInitialization()
        {
            improvement = GameDatabase.Instance.Improvements[improvementName];
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return improvement.IconGraphic;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override bool IsActivityValid(HexTile tile)
        {
            return improvement.CheckIfCanBuild(tile);
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Improvement = improvement;
        }

    }
}
