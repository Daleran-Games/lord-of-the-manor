using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;
using DaleranGames.TBSFramework;

namespace DaleranGames.Database
{
    public class GameDatabase : Singleton<GameDatabase>
    {

        protected GameDatabase() { }

        public System.Action DatabasesInitialized;

        public Database<TileGraphic> TileGraphics;
        public Database<LandType> LandTiles;
        public Database<ImprovementType> Improvements;

        public void InitializeDatabases()
        {
            TileGraphics = GetComponentInChildren<GraphicsDatabaseLoader>().GenerateDatabase();
            Debug.Log("Initialized graphics");
            LandTiles = GetComponentInChildren<LandDatabaseLoader>().GenerateDatabase();
            Debug.Log("Initialized land tiles");
            Improvements = GetComponentInChildren<ImprovementsDatabaseLoader>().GenerateDatabase();
            Debug.Log("Initialized improvements");

            GetComponentInChildren<GraphicsDatabaseLoader>().InitializeDatabase(TileGraphics);
            GetComponentInChildren<LandDatabaseLoader>().InitializeDatabase(LandTiles);
            GetComponentInChildren<ImprovementsDatabaseLoader>().InitializeDatabase(Improvements);

            Debug.Log("Initialized databases");

            if (DatabasesInitialized != null)
                DatabasesInitialized();

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}

