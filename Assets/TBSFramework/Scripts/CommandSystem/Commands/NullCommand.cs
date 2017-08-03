using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class NullCommand : Command
    {

        public NullCommand(string name, string type, int id)
        {
            this.name = name;
            this.type = type;
            this.id = id;
            activity = CommandType.None;
        }

        public override void PreformCommand(HexTile tile)
        {

        }
        public override bool IsValidCommand(HexTile tile)
        {
            return false;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public override TileGraphic GetWorkIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        protected override void Awake()
        {

        }

        public override void OnDatabaseInitialization()
        {

        }

    }
}
