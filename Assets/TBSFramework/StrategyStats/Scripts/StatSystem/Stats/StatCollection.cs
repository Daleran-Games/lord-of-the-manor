using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class StatCollection 
    {
        public ModifierCollection Modifiers;
        protected Dictionary<Stat.Category, int> stats;



        public void AddStat (Stat stat)
        {

        }

        public void ReplaceStat (Stat stat)
        {

        }

        public void RemoveStat(Stat stat)
        {

        }

        public void ClearStat(Stat.Category statCat)
        {

        }

        public bool ContainsStat (Stat.Category statCat)
        {

        }



    }
}