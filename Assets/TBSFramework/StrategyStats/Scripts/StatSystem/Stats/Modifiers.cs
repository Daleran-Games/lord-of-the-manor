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

        public ModifierDatabase<Unit> UnitModifiers = new ModifierDatabase<Unit>();
        public ModifierDatabase<HexTile> TileModifiers = new ModifierDatabase<HexTile>();
        public ModifierDatabase<TileType> TileTypeModifiers = new ModifierDatabase<TileType>();
        public ModifierDatabase<UnitType> UnitTypeModifiers = new ModifierDatabase<UnitType>();
        public ModifierDatabase<Cultures> CultureModifiers = new ModifierDatabase<Cultures>();
        public ModifierDatabase<Ranks> RankModifiers = new ModifierDatabase<Ranks>();
        public ModifierDatabase<Seasons> SeasonModifiers = new ModifierDatabase<Seasons>();
        public ModifierCollection GlobalModifiers = new ModifierCollection();

    }
}
