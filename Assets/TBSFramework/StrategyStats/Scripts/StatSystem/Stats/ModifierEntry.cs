using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class ModifierEntry
    {
        public string ModifiedStat = "Null";
        public int Amount = 0;
        public string Description = "None";


        public Modifier GetModifer()
        {
            return new Modifier(new Stat(Enumeration.FromDisplayName<StatType>(ModifiedStat), Amount), Description);
        }
        

    }
}