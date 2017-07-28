﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            GameDatabase.Instance.InitializeDatabases();
            grid.GenerateMap();

            GroupManager.Instance.SetUpGroupManager();

            timer.Stop();
            Debug.Log("TOTAL LOAD: Time: " + timer.ElapsedMilliseconds + " ms");

            OnMapBuildComplete();

        }

        void OnMapBuildComplete ()
        {
            if (StateDisabled != null)
                StateDisabled(this);
        }

        private void OnDisable()
        {

        }




    }
}