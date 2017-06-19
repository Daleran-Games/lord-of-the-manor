using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewLandType", menuName = "DaleranGames/TBS/Land Type", order = 0)]
    public class LandType : ScriptableObject
    {
        public string Name { get { return this.name; } }

        [SerializeField]
        Vector2Int atlasCoord = new Vector2Int(0, 0);
        public Vector2Int AtlasCoord { get { return atlasCoord; } }


    }
}
