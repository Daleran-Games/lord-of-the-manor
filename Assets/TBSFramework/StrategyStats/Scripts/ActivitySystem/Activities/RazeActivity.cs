using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class RazeActivity : Activity
    {
        protected TileGraphic icon;

        public RazeActivity(string[] csv)
        {
            id = Int32.Parse(csv[0]);
            name = csv[1];
            type = csv[2];
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnDatabaseInitialization()
        {
            icon = GameDatabase.Instance.TileGraphics["UIAtlas_Highlight_Cross"];
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return icon;
        }

        public override bool IsActivityValid(HexTile tile)
        {
            if (tile.Improvement != null)
                return true;
            else
                return false;
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Improvement = null;
        }


    }
}
