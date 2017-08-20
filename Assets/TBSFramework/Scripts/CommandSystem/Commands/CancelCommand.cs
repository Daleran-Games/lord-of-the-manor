using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class CancelCommand : Command
    {
        public CancelCommand()
        {

        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            ICancelable cancelable = tile.Feature as ICancelable;
            if (cancelable != null)
            {
                if (cancelable.CanCancel(tile) && owner == tile.Owner)
                    return true;
            }
            return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            ICancelable cancelable = tile.Feature as ICancelable;
            cancelable.Cancel(tile);
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_Cancel"];
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            ICancelable cancelable = tile.Feature as ICancelable;
            if (cancelable == null)
            {
                return (("Tile Cannot be Canceled").ToNegativeColor());
            }
            else
            {
                if (!cancelable.CanCancel(tile) || group != tile.Owner)
                    return ("Cannot Cancel " + tile.Feature.Name).ToNegativeColor();
                else
                    return ("Cancel " + tile.Feature.Name).ToPositiveColor();
            }
        }

    }
}
