using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class BuildCommand : Command
    {
        [SerializeField]
        string improvementName;

        [SerializeField]
        FeatureType improvement;

        public BuildCommand(CSVEntry entry)
        {
            this.id = entry.ID;
            name = entry["name"];
            type = entry["type"];
            commandIconName = entry["workIcon"];
            improvementName = entry["improvementName"];
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
            return TileGraphic.Clear;
        }

        public override bool IsValidCommand(HexTile tile)
        {
            return false;
        }

        public override void PreformCommand(HexTile tile)
        {
            tile.Feature = improvement;
        }

    }
}
