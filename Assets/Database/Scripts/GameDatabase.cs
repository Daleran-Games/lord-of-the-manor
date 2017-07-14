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
        //public Database<StatType> StatTypes;
        public Database<LandType> LandTiles;
        public Database<ImprovementType> Improvements;
        public Database<GroupType> Units;
        public Database<Activity> Activities;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void InitializeDatabases()
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            Sprites = GetComponentInChildren<SpriteDatabaseLoader>().GetSpriteDictionary();
            TileGraphics = GetComponentInChildren<GraphicsDatabaseLoader>().GenerateDatabase();
            //StatTypes = GetComponentInChildren<StatsDatabaseLoader>().GenerateDatabase();
            LandTiles = GetComponentInChildren<LandDatabaseLoader>().GenerateDatabase();
            Improvements = GetComponentInChildren<ImprovementsDatabaseLoader>().GenerateDatabase();
            Units = GetComponentInChildren<UnitsDatabaseLoader>().GenerateDatabase();
            Activities = GetComponentInChildren<ActivityDatabaseLoader>().GenerateDatabase();


            GetComponentInChildren<GraphicsDatabaseLoader>().InitializeDatabase(TileGraphics);
            //GetComponentInChildren<StatsDatabaseLoader>().InitializeDatabase(StatTypes);
            GetComponentInChildren<LandDatabaseLoader>().InitializeDatabase(LandTiles);
            GetComponentInChildren<ImprovementsDatabaseLoader>().InitializeDatabase(Improvements);
            GetComponentInChildren<UnitsDatabaseLoader>().InitializeDatabase(Units);
            GetComponentInChildren<ActivityDatabaseLoader>().InitializeDatabase(Activities);


            timer.Stop();
            Debug.Log("DATABASE: Initialization Time: " + timer.ElapsedMilliseconds + " ms");

            if (DatabasesInitialized != null)
                DatabasesInitialized();

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}

