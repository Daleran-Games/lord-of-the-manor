using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;
using DaleranGames.TBSFramework;

namespace DaleranGames
{
    public class GameManager : Singleton<GameManager>
    {
        protected GameManager ( ) { }

        public event Action<GameState> StateChanged;

        [SerializeField]
        private GameState currentState;
        public GameState CurrentState
        {
            get { return currentState; }
            private set { currentState = value; }
        }

        //Initialize Game State
        [SerializeField]
        private LoadGameState loadGame;
        public LoadGameState LoadGame
        {
            get { return loadGame; }
            private set { loadGame = value; }
        }

        //Menu State

        // Load Game State

        // New Game State

        [SerializeField]
        private LoadSceneState loadScene;
        public LoadSceneState LoadScene
        {
            get { return loadScene; }
            private set { loadScene = value; }
        }

        [SerializeField]
        private PlayState play;
        public PlayState Play
        {
            get { return play; }
            private set { play = value; }
        }

        //Battle State

        private void Awake()
        {
            DontDestroyOnLoad(this);

            LoadGame = gameObject.GetRequiredComponent<LoadGameState>();
            LoadScene = gameObject.GetRequiredComponent<LoadSceneState>();
            Play = gameObject.GetRequiredComponent<PlayState>();

            CurrentState = LoadGame;

            LoadGame.enabled = false;
            LoadScene.enabled = false;
            Play.enabled = false;
        }

        private void OnEnable()
        {

        }

        private void Start()
        {
            LoadScene.StateDisabled += OnLoadSceneComplete;
            LoadGame.StateDisabled += OnLoadGameComplete;

            CurrentState.enabled = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LoadScene.StateDisabled -= OnLoadSceneComplete;
            LoadGame.StateDisabled -= OnLoadGameComplete;
        }

        void ChangeState(GameState newState)
        {
            CurrentState.enabled = false;
            CurrentState = newState;
            CurrentState.enabled = true;

            if (StateChanged != null)
                StateChanged(newState);

            Debug.Log("Transitioning to: " + newState.GetType().ToString());
        }

        void OnLoadGameComplete(GameState newState)
        {
            ChangeState(LoadScene);
        }

        void OnLoadSceneComplete(GameState newState)
        {
            ChangeState(Play);
        }

    }

}
