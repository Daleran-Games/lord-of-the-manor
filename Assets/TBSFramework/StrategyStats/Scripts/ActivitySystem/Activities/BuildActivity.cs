using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewBuildActivity", menuName = "DaleranGames/TBS/Activities/Build", order = 0)]
    public class BuildActivity : Activity
    {
        [SerializeField]
        ImprovementType improvement;

        public override Vector2Int GetTerrainIcon(HexTile tile)
        {
            return improvement.IconGraphic;
        }

        public override Vector2Int GetUIIcon(HexTile tile)
        {
            return Vector2Int.zero;
        }

        public override bool IsActivityValid(HexTile tile)
        {
            return improvement.CheckIfCanBuild(tile);
        }

        public override void DoActivityOnTile(HexTile tile)
        {
            tile.Improvement = improvement;
        }


    }
}
