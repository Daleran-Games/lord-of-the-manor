using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;
using DaleranGames.TBSFramework;

namespace DaleranGames.IO
{
    public class GameDatabase : Singleton<GameDatabase>
    {

        protected GameDatabase() { }

        [SerializeField]
        protected TileAtlas atlas;
        public TileAtlas Atlas { get { return atlas; } }

#pragma warning disable 0649
        [Header("Database Loaders")]
        [SerializeField]
        SpriteDatabaseLoader spriteLoader;
        [SerializeField]
        GraphicsDatabaseLoader graphicsLoader;
        [SerializeField]
        LandDatabaseLoader landsLoader;
        [SerializeField]
        FeaturesDatabaseLoader featuresLoader;
        [SerializeField]
        GroupsDatabaseLoader groupsLoader;

#pragma warning restore 0649

        public Dictionary<string, Sprite> Sprites;
        public Database<TileGraphic> TileGraphics;
        public Database<LandType> Lands;
        public Database<FeatureType> Features;
        public Database<GroupType> Groups;

        public static readonly string SpritePath = "Assets/Graphics/Sprites/";
        public static string GameDataPath { get { return Application.streamingAssetsPath + "/GameData/"; } }


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void InitializeDatabases()
        {
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            Sprites = spriteLoader.GenerateDatabase();
            TileGraphics = graphicsLoader.GenerateDatabase();
            Lands = landsLoader.GenerateDatabase();
            Features = featuresLoader.GenerateDatabase();
            Groups = groupsLoader.GenerateDatabase();

            graphicsLoader.InitializeDatabase(TileGraphics);
            landsLoader.InitializeDatabase(Lands);
            featuresLoader.InitializeDatabase(Features);
            groupsLoader.InitializeDatabase(Groups);

            timer.Stop();
            Debug.Log("DATABASE: Initialization Time: " + timer.ElapsedMilliseconds + " ms");

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}

