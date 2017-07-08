using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Modifier 
    {
        public enum Scope
        {
            None = 0,
            Unit = 1,
            Tile = 2,
            Group =3,
            Global = 4
        }

        Scope scope;
        public Scope ModifierScope { get { return scope; } }

        Stat mod;
        public Stat Mod { get { return mod; } }

        string description;
        public string Description { get { return description; } }

        public Modifier (Scope scope, Stat mod)
        {
            this.scope = scope;
            this.mod = mod;
            description = "None";
        }

        public Modifier(Scope scope, Stat mod, string description)
        {
            this.scope = scope;
            this.mod = mod;
            this.description = description;
        }

    }
}
