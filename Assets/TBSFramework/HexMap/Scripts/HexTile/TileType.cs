using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public abstract class TileType : ScriptableObject
    {
        public abstract KeyValuePair<HexLayers,Vector2Int>[] Graphics { get; }
        public abstract Vector2Int GetGraphicsAtLayer(HexLayers layer);
    }
}
