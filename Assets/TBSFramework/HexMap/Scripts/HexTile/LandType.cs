using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;
using System;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewLandType", menuName = "DaleranGames/TBS/Land Type", order = 0)]
    public class LandType : TileType
    {
        [SerializeField]
        Vector2Int atlasCoord = new Vector2Int(0, 0);

        public override KeyValuePair<HexLayers, Vector2Int>[] Graphics
        {
            get
            {
                return new KeyValuePair<HexLayers, Vector2Int>[] { new KeyValuePair<HexLayers, Vector2Int>( HexLayers.Land,atlasCoord)};
            }
        }

        public override Vector2Int GetGraphicsAtLayer(HexLayers layer)
        {
            if (layer == HexLayers.Land)
                return atlasCoord;

            return Vector2Int.zero;
        }
    }
}
