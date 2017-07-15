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

        //Load Game State

        //Menu State

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

        private void Awake()
        {
            DontDestroyOnLoad(this);

            LoadScene = gameObject.GetRequiredComponent<LoadSceneState>();
            Play = gameObject.GetRequiredComponent<PlayState>();

            CurrentState = LoadScene;

            LoadScene.enabled = false;
            Play.enabled = false;
        }

        private void OnEnable()
        {

        }

        private void Start()
        {
            LoadScene.StateDisabled += OnMapGenerationComplete;

            CurrentState.enabled = true;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            LoadScene.StateDisabled -= OnMapGenerationComplete;
        }

        void ChangeState(GameState newState)
        {
            CurrentState.enabled = false;
            CurrentState = newState;
            CurrentState.enabled = true;

            if (StateChanged != null)
                StateChanged(newState);

            //Debug.Log("Transitioning to: " + newState.GetType().ToString());
        }

        void OnMapGenerationComplete(GameState newState)
        {
            ChangeState(Play);
        }

    }

}
