using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.UI;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class WorkCommand : Command
    {
        public WorkCommand()
        {

        }

        public override bool IsValidCommand(HexTile tile, Group owner)
        {
            IWorkable workable = tile.Feature as IWorkable;
            if (workable != null && owner == tile.Owner)
            {
                if (tile.Work.Paused && workable.CanResume(tile))
                    return true;
                else if (!tile.Work.Paused)
                    return true;
            }
            return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            IWorkable workable = tile.Feature as IWorkable;

            if (tile.Work.Paused)
            {
                workable.Resume(tile);
                tile.Work.PausedOverride = false;
            }
            else
            {
                workable.Pause(tile);
                tile.Work.PausedOverride = true;
            }
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            if (tile.Work.Paused)
                return GameDatabase.Instance.TileGraphics["Icon_32px_Work"];
            else
                return GameDatabase.Instance.TileGraphics["Icon_16px_Sleep"]; 
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            IWorkable workable = tile.Feature as IWorkable;
            if (workable == null)
            {
                return (("Tile Not Capable be Worked Or Paused").ToNegativeColor());
            }
            else
            {
                if (!tile.Work.Paused)
                    return (("Pause Tile").ToPositiveColor());
                else
                {
                    if (workable.CanResume(tile))
                        return (("Resume Tile").ToPositiveColor());
                    else
                        return (("Cannot Resume Tile").ToNegativeColor());
                }
            }
        }
    }
}
