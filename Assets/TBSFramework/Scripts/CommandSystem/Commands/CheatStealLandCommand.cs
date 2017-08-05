using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CheatStealLandCommand : Command
    {
        public CheatStealLandCommand()
        {

        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            tile.Owner = owner;
        }
        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            if (tile.Owner != owner)
                return true;
            else
                return false;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_PlayerTerritory"];
            
        }

        public override TileGraphic GetTerrainIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

    }
}