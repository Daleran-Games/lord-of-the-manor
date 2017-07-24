using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class NullActivity : Activity
    {

        public NullActivity(string name, string type, int id)
        {
            this.name = name;
            this.type = type;
            this.id = id;
        }

        public override void DoActivityOnTile(HexTile tile)
        {

        }
        public override bool IsActivityValid(HexTile tile)
        {
            return false;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            return TileGraphic.clear;
        }

        protected override void Awake()
        {

        }

        public override void OnDatabaseInitialization()
        {

        }

    }
}
