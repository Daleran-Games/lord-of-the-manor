using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Game;

namespace DaleranGames.TBSFramework
{
    public abstract class BaseTurn : GameState
    {
        public abstract void NextTurn();
    }
}
