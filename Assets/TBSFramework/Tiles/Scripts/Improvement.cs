using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Improvement
    {
        [SerializeField]
        [ReadOnly]
        ImprovementType improvementType;
        public ImprovementType HexImprovementType { get { return improvementType; } set { improvementType = value; } }

        public Improvement ()
        {

        }

    }
}