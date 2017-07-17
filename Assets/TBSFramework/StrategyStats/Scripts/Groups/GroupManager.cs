using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    public class GroupManager : Singleton<GroupManager>
    {
        protected GroupManager() { }

        [SerializeField]
        protected Group playerGroup;


        public void Start()
        {
            if (GameManager.Instance.CurrentState is PlayState)
            {
                OnGameStart(GameManager.Instance.CurrentState);

            }

            GameManager.Instance.Play.StateEnabled += OnGameStart;
            TurnManager.Instance.TurnEnded += OnTurnEnd;
            TurnManager.Instance.TurnSetUp += OnTurnSetUp;
            TurnManager.Instance.TurnStart += OnTurnStart;
        }


        public virtual void OnGameStart(GameState state)
        {

        }

        public virtual void OnTurnEnd(BaseTurn turn)
        {

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

