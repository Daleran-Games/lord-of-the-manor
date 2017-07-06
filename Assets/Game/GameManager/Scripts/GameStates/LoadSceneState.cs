using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using DaleranGames.Database;

namespace DaleranGames.Game
{
    public class LoadSceneState : GameState
    {
        [SerializeField]
        HexGrid grid;

        private void OnEnable()
        {
            if (StateEnabled != null)
                StateEnabled(this);

            grid.MeshBuildComplete += OnMapBuildComplete;

            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            GameDatabase.Instance.InitializeDatabases();
            grid.GenerateMap();

            timer.Stop();
            Debug.Log("TOTAL LOAD: Time: " + timer.ElapsedMilliseconds + " ms");

        }

        void OnMapBuildComplete ()
        {
            if (StateDisabled != null)
                StateDisabled(this);
        }

        private void OnDisable()
        {
            grid.MeshBuildComplete -= OnMapBuildComplete;
        }




    }
}
