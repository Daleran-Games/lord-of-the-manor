using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class WorkSeasonCommand : Command
    {
        Seasons season;
        bool work;

        public WorkSeasonCommand(Seasons season, bool work)
        {
            this.season = season;
            this.work = work;
        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            ISeasonable seasonable = tile.Feature as ISeasonable;
            if (seasonable != null && owner == tile.Owner)
            {
                return true;
            }
            return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            ISeasonable seasonable = tile.Feature as ISeasonable;
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return TileGraphic.Clear;
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            ISeasonable seasonable = tile.Feature as ISeasonable;
            if (seasonable == null)
            {
                return (("Tile Not Capable of being toggled by season").ToNegativeColor());
            }
            else
            {
                return "Test";
            }
        }
    }
}
