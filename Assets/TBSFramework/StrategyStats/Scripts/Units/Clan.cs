using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Clan 
    {
        [SerializeField]
        Unit playerUnit;
        [SerializeField]
        List<Unit> tenets;
        [SerializeField]
        Stat testStat;
        [SerializeField]
        Vector2Int test2;

    }
}