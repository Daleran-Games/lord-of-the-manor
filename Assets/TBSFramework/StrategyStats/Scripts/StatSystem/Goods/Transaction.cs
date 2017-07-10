using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Transaction 
    {

        bool immediate;
        public bool Immediate { get { return immediate; } }

        Good good;
        public Good TransactedGood { get { return TransactedGood; } }

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