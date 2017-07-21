using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    public abstract class GameState : MonoBehaviour
    {
        public Action<GameState> StateEnabled;
        public Action<GameState> StateDisabled;

    }
}
