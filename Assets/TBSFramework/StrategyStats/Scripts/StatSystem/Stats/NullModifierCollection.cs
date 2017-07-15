﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class NullModifierCollection : IModifierCollection
    {
        public int this[StatType statType]
        {
            get
            {
                return 0;
            }
        }

        public void Add(Modifier mod)
        {

        }

        public void Add(ModifierEntry[] mods)
        {

        }

        public void Clear(StatType statType)
        {

        }

        public void ClearAll()
        {

        }

        public Modifier[] GetAll(StatType statType)
        {
            return new Modifier[0];
        }

        public void Remove(Modifier mod)
        {
            
        }

        public void Remove(ModifierEntry[] mods)
        {

        }
    }
}
