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
            playerGroup = new Group("Player", GameDatabase.Instance.Groups["Thegn"]);
            
            GameManager.Instance.Play.StateEnabled += OnGameStart;
            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
        }

        public void Start()
        {


            if (GameManager.Instance.CurrentState == GameManager.Instance.Play)
                OnGameStart(GameManager.Instance.CurrentState);
        }

        public virtual void OnGameStart(GameState state)
        {

        }

        public virtual void OnTurnEnd(BaseTurn turn)
        {
            playerGroup.Goods.ProcessAllTransactions();
            playerGroup.Goods.ResolveEdgeCases();
        }

        public virtual void OnTurnSetUp(BaseTurn turn)
        {

        }

        public virtual void OnTurnStart(BaseTurn turn)
        {

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

