using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewRazeActivity", menuName = "DaleranGames/TBS/Activities/Raze", order = 0)]
    public class RazeActivity : Activity
    {
        protected TileGraphic icon;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnDatabaseInitialization()
        {
            icon = GameDatabase.Instance.TileGraphics.Get("UI Cursor Cross");
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
