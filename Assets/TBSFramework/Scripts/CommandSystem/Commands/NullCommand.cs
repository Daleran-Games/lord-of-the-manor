using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class NullCommand : Command
    {

        public NullCommand()
        {

        }

        public override string GetInfo(HexTile tile, Group group)
        {
            return "Null Command".ToNegativeColor();
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {

        }
        public override bool IsValidCommand(HexTile tile, Group owner)
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

    }
}
