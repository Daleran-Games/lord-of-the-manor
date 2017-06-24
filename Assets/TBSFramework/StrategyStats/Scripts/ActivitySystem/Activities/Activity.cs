using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public abstract class Activity : ScriptableObject
    {
        public abstract void DoActivityOnTile(HexTile tile);
        public abstract bool IsActivityValid(HexTile tile);
        public abstract Vector2Int GetUIIcon(HexTile tile);
        public abstract Vector2Int GetTerrainIcon(HexTile tile);
    }
}

