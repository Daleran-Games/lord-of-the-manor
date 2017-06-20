using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewImprovementType", menuName = "DaleranGames/TBS/Improvement Type", order = 0)]
    public class ImprovementType : TileType
    {
        [SerializeField]
        Vector2Int atlasCoord = new Vector2Int(0, 0);

        public override KeyValuePair<HexLayers, Vector2Int>[] Graphics
        {
            get
            {
                return new KeyValuePair<HexLayers, Vector2Int>[] { new KeyValuePair<HexLayers, Vector2Int>(HexLayers.Improvements, atlasCoord) };
            }
        }

        public override Vector2Int GetGraphicsAtLayer(HexLayers layer)
        {
            if (layer == HexLayers.Improvements)
                return atlasCoord;
            else
            {
                Debug.LogError("TILE ERROR: Requested graphics of wrong layer for a ImprovementType");
            }
            return Vector2Int.zero;
        }
    }
}