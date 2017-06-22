using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Game;

namespace DaleranGames.TBSFramework
{
    public abstract class TileType : ScriptableObject
    {
        public virtual void OnActivation(HexTile tile)
        {
            if (GameManager.Instance.CurrentState is PlayState)
            {
                OnGameStart(tile);
            }
        }

        public virtual void OnGameStart(HexTile tile)
        {

        }

        public virtual void OnTurnChange(BaseTurn turn, HexTile tile)
        {

        }

        public virtual void OnDeactivation(HexTile tile)
        {

        }
    
    }
}
