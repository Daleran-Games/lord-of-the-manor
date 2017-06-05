using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    public interface IStat
    {
        StatType Type { get; }
        int Value { get; }
        Action<int, int> ValueChanged { get; set; }
    }
}

