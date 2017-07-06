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

        public RazeActivity(RazeActivity activity, int id)
        {
            name = activity.Name;
            this.id = id;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnDatabaseInitialization()
        {
            icon = GameDatabase.Instance.TileGraphics.Get("UIAtlas_Highlight_Cross");
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

        public override string ToJson()
        {
            this.type = this.ToString();
            return JsonUtility.ToJson(this, true);
        }
    }
}
