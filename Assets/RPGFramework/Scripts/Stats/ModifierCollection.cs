using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class ModifierCollection
    {

        protected List<ModifierSet> modifiers;

        public ModifierCollection ()
        {
            modifiers = new List<ModifierSet>();
        }



    }
}