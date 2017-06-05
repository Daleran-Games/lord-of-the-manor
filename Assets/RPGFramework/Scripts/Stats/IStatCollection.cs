using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    public interface IStatCollection
    {
        Stat[] GetAllStats();
        bool ContainsStat(StatType type);
        T GetStat<T>() where T : Stat;
    } 
}
