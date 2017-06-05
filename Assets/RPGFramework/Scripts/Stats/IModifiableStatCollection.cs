using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    public interface IModifiableStatCollection : IStatCollection
    {
        ModifierList Modifiers { get; set; }
        ModifiableStat[] GetModifiableStats();
    }
}