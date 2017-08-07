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
        List<T> Types { get; }
        List<Modifier> GetAllOfType(T statType);
        List<Modifier> GetAll();
        bool Contains(T statType);
        void Add(Modifier mod);
        void Add(IList<Modifier> mods);
        void Remove(Modifier mod);
        void Remove(IList<Modifier> mods);
        void Clear(T statType);
        void ClearAll();
    }

}

