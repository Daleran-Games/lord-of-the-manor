using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewRazeActivity", menuName = "DaleranGames/TBS/Activities/Raze", order = 0)]
    public class RazeActivity : Activity
    {
        public override Vector2Int GetTerrainIcon(HexTile tile)
        {
            return Vector2Int.zero;
        }

        public override Vector2Int GetUIIcon(HexTile tile)
        {
            return new Vector2Int(7, 0);
        }


        public override bool IsActivityValid(HexTile tile)
        {
            if (tile.Improvement != null)
                return true;
            else
                return false;
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Improvement = null;
        }
    }
}
