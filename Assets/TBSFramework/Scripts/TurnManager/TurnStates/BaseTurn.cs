using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public abstract class BaseTurn : GameState
    {
        public abstract Seasons Season { get; }
        public abstract void NextTurn();
    }
}
