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

        [SerializeField]
        protected TileAtlas atlas;
        public TileAtlas Atlas { get { return atlas; } }
        public Dictionary<string, Sprite> Sprites;
        public Database<TileGraphic> TileGraphics;
        public Database<LandType> LandTiles;
        public Database<ImprovementType> Improvements;
        public Database<Activity> Activities;

        public void InitializeDatabases()
        {
            Sprites = GetComponentInChildren<SpriteDatabaseLoader>().GetSpriteDictionary();
            TileGraphics = GetComponentInChildren<GraphicsDatabaseLoader>().GenerateDatabase();
            Debug.Log("Built graphics");
            LandTiles = GetComponentInChildren<LandDatabaseLoader>().GenerateDatabase();
            Debug.Log("Built land tiles");
            Improvements = GetComponentInChildren<ImprovementsDatabaseLoader>().GenerateDatabase();
            Debug.Log("Built improvements");
            Activities = GetComponentInChildren<ActivityDatabaseLoader>().GenerateDatabase();
            Debug.Log("Built activities");


            GetComponentInChildren<GraphicsDatabaseLoader>().InitializeDatabase(TileGraphics);
            GetComponentInChildren<LandDatabaseLoader>().InitializeDatabase(LandTiles);
            GetComponentInChildren<ImprovementsDatabaseLoader>().InitializeDatabase(Improvements);
            GetComponentInChildren<ActivityDatabaseLoader>().InitializeDatabase(Activities);


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

