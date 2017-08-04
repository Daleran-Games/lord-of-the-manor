using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                workable.Resume(tile);
            else
                workable.Pause(tile);

        }

        public override TileGraphic GetUIIcon(HexTile tile)
        {
            return GameDatabase.Instance.TileGraphics["UIAtlas_Icon_AnvilTool"];
        }
    }
}
