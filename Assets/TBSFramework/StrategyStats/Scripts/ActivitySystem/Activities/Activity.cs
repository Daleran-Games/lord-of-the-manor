using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Database;

namespace DaleranGames.TBSFramework
{
    public abstract class Activity : ScriptableObject
    {
        public abstract void DoActivityOnTile(HexTile tile);
        public abstract bool IsActivityValid(HexTile tile);
        public abstract TileGraphic GetUIIcon(HexTile tile);
        public abstract TileGraphic GetTerrainIcon(HexTile tile);

        protected virtual void Awake()
        {
            GameDatabase.Instance.DatabasesInitialized += OnDatabaseInitialization;
        }

        public virtual void OnDatabaseInitialization()
        {

        }
    }
}

