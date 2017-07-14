using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Transaction 
    {
        [SerializeField]
        bool immediate;
        public bool Immediate { get { return immediate; } }

        [SerializeField]
        Good good;
        public Good TransactedGood { get { return TransactedGood; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Transaction (bool immediate,Good good)
        {
            this.good = good;
            this.immediate = immediate;
            description = "None";
        }

        public Transaction(bool immediate, Good good, string description)
        {
            this.good = good;
            this.immediate = immediate;
            this.description = description;
        }

    }
}