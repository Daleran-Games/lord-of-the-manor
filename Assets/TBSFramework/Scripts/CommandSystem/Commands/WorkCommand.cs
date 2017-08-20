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
                if (tile.Paused && workable.CanResume(tile))
                    return true;
                else if (!tile.Paused)
                    return true;
            }
            return false;
        }

        public override void PreformCommand(HexTile tile, Group owner)
        {
            IWorkable workable = tile.Feature as IWorkable;

            if (tile.Paused)
            {
                workable.Resume(tile);
                tile.PausedOverride = false;
            }
            else
            {
                workable.Pause(tile);
                tile.PausedOverride = true;
            }
        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            if (tile.Paused)
                return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_LargeHammer"];
            else
                return GameDatabase.Instance.TileGraphics["UIAtlas_SmallIcon_Sleep"]; 
        }

        public override string GetInfo(HexTile tile, Group group)
        {
            IWorkable workable = tile.Feature as IWorkable;
            if (workable == null)
            {
                return (("Tile Cannot be Worked Or Paused").ToNegativeColor());
            }
            else
            {
                if (!tile.Paused)
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
