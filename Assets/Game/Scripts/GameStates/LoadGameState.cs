using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using DaleranGames.IO;

namespace DaleranGames
{
    public class LoadGameState : GameState
    {
        private void OnEnable()
        {
            if (StateEnabled != null)
                StateEnabled(this);

            GameDatabase.Instance.InitializeDatabases();

            if (StateDisabled != null)
                StateDisabled(this);
        }

        private void OnDisable()
        {

        }

    }
}
