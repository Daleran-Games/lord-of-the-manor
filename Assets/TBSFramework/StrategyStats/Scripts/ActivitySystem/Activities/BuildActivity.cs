using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewBuildActivity", menuName = "DaleranGames/TBS/Activities/Build", order = 0)]
    public class BuildActivity : Activity
    {
        [SerializeField]
        string improvementName;
        ImprovementType improvement;

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
