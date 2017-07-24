using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace DaleranGames.TBSFramework
{
    public interface IStatCollection<T> where T : StatType
    {
        event Action <IStatCollection<T>,T> StatModified;
        int this[T statType] { get; }
        T[] Types { get; }
        Modifier[] GetAllOfType(T statType);
        bool Contains(T statType);
        void Add(Modifier mod);
        void Add(Modifier[] mods);
        void Remove(Modifier mod);
        void Remove(Modifier[] mods);
        void Clear(T statType);
        void ClearAll();
    }

}

