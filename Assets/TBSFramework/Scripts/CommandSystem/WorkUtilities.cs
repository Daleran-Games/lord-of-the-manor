using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class WorkUtilities
    {
        Queue<HexTile> pausedTiles;

        public WorkUtilities()
        {
            pausedTiles = new Queue<HexTile>();
        }


        public void OptimizeLabor(Group group)
        {
            if (group.Goods.Labor.Value < 0)
            {
                List<HexTile> workedTiles = GeWorkabletHexTilesSortedByUtility(group.OwnedTiles, false);
                while (group.Goods.Labor.Value < 0 && workedTiles.Count > 0)
                {
                    IWorkable work = workedTiles[0].Feature as IWorkable;
                    work.Pause(workedTiles[0]);
                    pausedTiles.Enqueue(workedTiles[0]);
                    workedTiles.Remove(workedTiles[0]);
                }
            } else
            {
                if (pausedTiles.Count > 0)
                {
                    HexTile nextTile = pausedTiles.Peek();
                    IWorkable work = nextTile.Feature as IWorkable;

                    while (-work.GetLaborWorkCosts(nextTile) >= group.Goods.Labor.Value && pausedTiles.Count > 0)
                    {
                        HexTile resumed = pausedTiles.Dequeue();
                        IWorkable resumedWork = resumed.Feature as IWorkable;

                        if (resumedWork.CanResume(resumed))
                            resumedWork.Resume(resumed);
                        else
                            pausedTiles.Enqueue(resumed);


                        if (pausedTiles.Count < 1)
                            break;

                        nextTile = pausedTiles.Peek();
                        work = nextTile.Feature as IWorkable;
                    }
                }
            }
        }

        List<HexTile> GeWorkabletHexTilesSortedByUtility(List<HexTile> tiles, bool paused)
        {
            List<HexTile> workableTiles = new List<HexTile>();
            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].Feature is IWorkable && !tiles[i].PausedOverride && tiles[i].Paused == paused)
                {
                    IWorkable work = tiles[i].Feature as IWorkable;
                    workableTiles.Add(tiles[i]);
                }
            }
            workableTiles.Sort(new UtilityComparer());
            return workableTiles;
        }

        public class UtilityComparer : IComparer<HexTile>
        {

            public int Compare(HexTile x, HexTile y)
            {
                IWorkable xWork = x.Feature as IWorkable;
                IWorkable yWork = y.Feature as IWorkable;

                if (xWork.GetWorkUtility(x) == yWork.GetWorkUtility(y))
                    return 0;
                else if (xWork.GetWorkUtility(x) < yWork.GetWorkUtility(y))
                    return -1;
                else
                    return 1;

            }
        }

    }
}
