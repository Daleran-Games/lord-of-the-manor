using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class WorkTileActivity : Activity
    {
        public WorkTileActivity(CSVData data, int id)
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

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override bool IsActivityValid(HexTile tile)
        {
            return tile.Land.Clearable(tile);
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            //tile.Land.ClearTile(tile);
        }

    }
}
