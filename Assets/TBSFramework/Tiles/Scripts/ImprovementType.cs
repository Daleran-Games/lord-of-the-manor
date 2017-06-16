using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    [CreateAssetMenu(fileName = "NewImprovementType", menuName = "DaleranGames/TBS/Improvement Type", order = 0)]
    public class ImprovementType : ScriptableObject
    {
        public string Name { get { return this.name; } }

        [SerializeField]
        Vector2Int atlasCoord = new Vector2Int(0, 0);
        public Vector2Int AtlasCoord { get { return atlasCoord; } }


    }
}