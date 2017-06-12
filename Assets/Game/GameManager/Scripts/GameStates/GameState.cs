using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.Game
{
    public abstract class GameState : MonoBehaviour
    {
        public Action<GameState> StateEnabled;
        public Action<GameState> StateDisabled;

    }
}
