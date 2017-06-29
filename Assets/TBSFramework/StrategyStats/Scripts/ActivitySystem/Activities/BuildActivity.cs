﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class BuildActivity : Activity
    {
        [SerializeField]
        protected string improvementName;
        public string ImprovementName { get { return improvementName; } }

        [System.NonSerialized]
        ImprovementType improvement;

        public BuildActivity(string name, int id, string impName)
        {
            this.name = name;
            this.id = id;
            improvementName = impName;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnDatabaseInitialization()
        {
            improvement = GameDatabase.Instance.Improvements.Get(improvementName);
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
