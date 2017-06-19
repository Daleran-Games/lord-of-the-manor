using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Land
    {
        [SerializeField]
        [ReadOnly]
        LandType landType;
        public LandType HexLandType { get { return landType; } set { landType = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte elevation = 0;
        public byte Elevation { get { return elevation; } set { elevation = value; } }

        [SerializeField]
        [ReadOnly]
        protected byte moisture = 0;
        public byte Moisture { get { return moisture; } set { moisture = value; } }

        public Land ()
        {

        }

    }
}
