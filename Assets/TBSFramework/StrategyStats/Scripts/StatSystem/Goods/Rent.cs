using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Rent 
    {
        [SerializeField]
        protected Transaction amount;
        public Transaction Amount { get { return amount; } }
    }
}
