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

        public Action<BaseTurn> TurnEnded;
        public Action<BaseTurn> TurnBegan;

        [SerializeField]
        protected int turn = 0;
        public int Turn { get { return turn; } protected set { turn = value; } }

        [SerializeField]
        protected int year = 687;
        public int Year { get { return year; } protected set { year = value; } }

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
            spring = gameObject.GetComponentInChildren<SpringTurn>();
            summer = gameObject.GetComponentInChildren<SummerTurn>();
            fall = gameObject.GetComponentInChildren<FallTurn>();
            winter = gameObject.GetComponentInChildren<WinterTurn>();
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
            ChangeTurn(Spring);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public void ChangeTurn(BaseTurn newState)
        {
            if (TurnEnded != null)
                TurnEnded(currentTurn);


            currentTurn.enabled = false;
            currentTurn = newState;
            currentTurn.enabled = true;
            Turn++;

            if (newState is SpringTurn)
                Year++;

            if (TurnBegan != null)
                TurnBegan(newState);

            //Debug.Log("Transitioning to: " + newState.GetType().ToString());
        }

        public void EndTurn()
        {
            CurrentTurn.NextTurn();
        }

        public static string SeasonString(BaseTurn turn)
        {
            if (turn is SpringTurn)
                return "Spring";
            else if (turn is SummerTurn)
                return "Summer";
            else if (turn is FallTurn)
                return "Fall";
            else if (turn is WinterTurn)
                return "Winter";
            else
                return "Error";
        }

    }
}
