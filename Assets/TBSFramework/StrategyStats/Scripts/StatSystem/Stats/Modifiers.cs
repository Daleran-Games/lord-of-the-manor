using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Modifiers : Singleton<Modifiers>
    {
        private Modifiers() { }

        public ModifierCollection<Unit> UnitModifiers = new ModifierCollection<Unit>();
        public ModifierCollection<HexTile> TileModifiers = new ModifierCollection<HexTile>();
        public ModifierCollection<Group> GroupModifiers = new ModifierCollection<Group>();
        public ModifierDictionary GlobalModifiers = new ModifierDictionary();

    }
}
