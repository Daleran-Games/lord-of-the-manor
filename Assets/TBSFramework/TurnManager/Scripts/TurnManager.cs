using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.Tools;

namespace DaleranGames.TBSFramework
{
    public class TurnManager : Singleton<TurnManager>
    {
        protected TurnManager() { }

        public Action<BaseTurn> TurnChanged;

        protected int turn = 0;
        public int Turn { get { return turn; } }

        [SerializeField]
        BaseTurn currentTurn;
        public BaseTurn CurrentTurn { get { return currentTurn; } }

        [SerializeField]
        SpringTurn spring;
        public SpringTurn Spring { get { return spring; } }

        [SerializeField]
        SummerTurn summer;
        public SummerTurn Summer { get { return summer; } }

        [SerializeField]
        FallTurn fall;
        public FallTurn Fall { get { return fall; } }

        [SerializeField]
        WinterTurn winter;
        public WinterTurn Winter { get { return winter; } }

        private void Awake()
        {
            spring = gameObject.GetRequiredComponent<SpringTurn>();
            summer = gameObject.GetRequiredComponent<SummerTurn>();
            fall = gameObject.GetRequiredComponent<FallTurn>();
            winter = gameObject.GetRequiredComponent<WinterTurn>();
        }

        private void OnEnable()
        {
            currentTurn = Spring;

            spring.enabled = false;
            summer.enabled = false;
            fall.enabled = false;
            winter.enabled = false;
        }

        private void Start()
        {
            currentTurn.enabled = true;
        }

        protected override void OnDestroy()
        {

        }

        public void ChangeTurn(BaseTurn newState)
        {
            currentTurn.enabled = false;
            currentTurn = newState;
            currentTurn.enabled = true;

            if (TurnChanged != null)
                TurnChanged(newState);

            //Debug.Log("Transitioning to: " + newState.GetType().ToString());
        }

        public void EndTurn()
        {
            CurrentTurn.NextTurn();
        }

    }
}
