using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DaleranGames.TBSFramework;
using DaleranGames.IO;

namespace DaleranGames
{
    public class LoadSceneState : GameState
    {
#pragma warning disable 0649
        [SerializeField]
        HexGrid grid;

#pragma warning restore 0649

        private void OnEnable()
        {
            if (StateEnabled != null)
                StateEnabled(this);
            SceneManager.LoadScene("Play",LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnSceneLoadComplete;

        }

        void OnSceneLoadComplete(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoadComplete;

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            grid = FindObjectOfType<HexGrid>();

            grid.GenerateMap();

            GroupManager.Instance.SetUpGroupManager();

            timer.Stop();
            Debug.Log("TOTAL LOAD: Time: " + timer.ElapsedMilliseconds + " ms");

            if (StateDisabled != null)
                StateDisabled(this);
        }

        private void OnDisable()
        {

        }




    }
}
