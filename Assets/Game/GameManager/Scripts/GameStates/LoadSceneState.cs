using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;

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

            grid.GenerateMap();

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
