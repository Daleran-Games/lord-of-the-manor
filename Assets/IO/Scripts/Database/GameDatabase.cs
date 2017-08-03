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

        public event System.Action DatabasesInitialized;

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
        ImprovementsDatabaseLoader improvementsLoader;
        [SerializeField]
        GroupsDatabaseLoader groupsLoader;
        [SerializeField]
        CommandDatabaseLoader commandLoader;
#pragma warning restore 0649

        public Dictionary<string, Sprite> Sprites;
        public Database<TileGraphic> TileGraphics;
        public Database<LandType> Lands;
        public Database<FeatureType> Improvements;
        public Database<GroupType> Groups;
        public Database<Command> Commands;

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
            Improvements = improvementsLoader.GenerateDatabase();
            Groups = groupsLoader.GenerateDatabase();
            Commands = commandLoader.GenerateDatabase();

            graphicsLoader.InitializeDatabase(TileGraphics);
            landsLoader.InitializeDatabase(Lands);
            improvementsLoader.InitializeDatabase(Improvements);
            groupsLoader.InitializeDatabase(Groups);
            commandLoader.InitializeDatabase(Commands);



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

