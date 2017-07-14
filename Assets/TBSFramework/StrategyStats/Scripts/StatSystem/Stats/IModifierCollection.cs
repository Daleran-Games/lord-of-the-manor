using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DaleranGames.TBSFramework
{
    public interface IModifierCollection
    {
        int this[StatType statType] { get; }
        Modifier[] GetAll(StatType statType);
        void Add(Modifier mod);
        void Add(ModifierEntry[] mods);
        void Remove(Modifier mod);
        void Remove(ModifierEntry[] mods);
        void Clear(StatType statType);
        void ClearAll();
    }

}

