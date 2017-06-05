using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public abstract class ModifierSet
    {
        [SerializeField]
        protected List<Modifier> modifiers;
        public List<Modifier> Modifiers { get { return modifiers; } }
        
        ModifierSet (params Modifier[] mods)
        {
            modifiers = new List<Modifier>();
            modifiers.AddRange(mods);
        }



    }
}

