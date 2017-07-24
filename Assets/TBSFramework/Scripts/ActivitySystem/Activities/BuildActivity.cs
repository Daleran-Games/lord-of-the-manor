using System;
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

        public BuildActivity(CSVData data, int id)
        {
            this.id = id;
            name = data["name", id];
            workIconName = data["workIcon", id];
            type = data["type", id];
            improvementName = data["improvementName", id];
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
            return improvement.CanBuildOnTile(tile);
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Improvement = improvement;
        }

    }
}
