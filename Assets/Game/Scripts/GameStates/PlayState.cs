using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using DaleranGames.UI;


namespace DaleranGames
{
    public class PlayState : GameState
    {

        private void OnEnable()
        {
            if (StateEnabled != null)
                StateEnabled(this);

            TurnManager.Instance.enabled = true;
            
        }




    }
}
