using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    public class GroupManager : Singleton<GroupManager>
    {
        protected GroupManager() { }

        [SerializeField]
        protected Group playerGroup;
        public Group PlayerGroup { get { return playerGroup; } }

        public void SetUpGroupManager()
        {
            playerGroup = new Group("Player", GameDatabase.Instance.Groups["Cheat"]);
            
            GameManager.Instance.Play.StateEnabled += OnGameStart;
            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;

            List<HexCoordinates> playerTiles = HexCoordinates.GetCoordinatesInRange(HexGrid.Instance.Generator.PlayerStart, HexGrid.Instance.Generator.PlayerTerritory);
            //Debug.Log("playerTiles count: " + playerTiles.Length);
            for (int i = 0; i < playerTiles.Count; i++)
            {
                if (HexGrid.Instance.IsCoordinateValid(playerTiles[i]))
                {
                    HexGrid.Instance[playerTiles[i]].Owner = PlayerGroup;
                }
            }

        }

        public virtual void OnGameStart(GameState state)
        {
            playerGroup.OnGameStart(state);
        }

        public virtual void OnTurnEnd(BaseTurn turn)
        {
            playerGroup.OnTurnEnd(turn);
        }

        public virtual void OnTurnSetUp(BaseTurn turn)
        {
            playerGroup.OnTurnSetUp(turn);
        }

        public virtual void OnTurnStart(BaseTurn turn)
        {
            playerGroup.OnTurnStart(turn);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameManager.Instance.Play.StateEnabled -= OnGameStart;
            TurnManager.Instance.TurnEnded -= OnTurnEnd;
            TurnManager.Instance.TurnSetUp -= OnTurnSetUp;
            TurnManager.Instance.TurnStart -= OnTurnStart;

        }
    }
}

