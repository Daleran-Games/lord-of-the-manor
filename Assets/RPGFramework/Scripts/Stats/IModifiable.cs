using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    public interface IModifiable
    {
        int BaseValue { get; }
        int ModifiedValue { get; set; }
        Action<int, int> ModifiedValueChanged { get; set; }
    }
}

