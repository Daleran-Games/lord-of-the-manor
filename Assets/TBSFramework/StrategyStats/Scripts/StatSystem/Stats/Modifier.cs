using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Modifier 
    {
        [SerializeField]
        Stat mod;
        public Stat Mod { get { return mod; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Modifier (Stat mod)
        {
            this.mod = mod;
            description = "None";
        }

        public Modifier(Stat mod, string description)
        {
            this.mod = mod;
            this.description = description;
        }

    }
}
