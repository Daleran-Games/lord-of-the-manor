using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public abstract class GoodsStorage
    {

        public abstract int Amount { get; }
        public abstract int Max { get; }

        

    }
}
