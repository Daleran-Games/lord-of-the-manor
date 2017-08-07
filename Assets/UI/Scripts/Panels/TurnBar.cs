using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.TBSFramework;
using TMPro;

namespace DaleranGames.UI
{
    public class TurnBar : MonoBehaviour
    {

        [SerializeField]
        protected TextMeshProUGUI turn;

        [SerializeField]
        protected TextMeshProUGUI year;

        [SerializeField]
        protected Image yearIcon;

        [SerializeField]
        protected Sprite spring;

        [SerializeField]
        protected Sprite summer;

        [SerializeField]
        protected Sprite fall;

        [SerializeField]
        protected Sprite winter;


        // Use this for initialization
        void Start()
        {
            TurnManager.Instance.TurnSetUp += OnTurnChange;
        }

        private void OnDestroy()
        {
            TurnManager.Instance.TurnSetUp -= OnTurnChange;
        }

        // Update is called once per frame
        void OnTurnChange (BaseTurn newTurn)
        {
            turn.text = "Turn " + TurnManager.Instance.Turn;
            year.text = TurnManager.SeasonString(newTurn) + " " + TurnManager.Instance.Year;
            yearIcon.sprite = SeasonSprite(newTurn);
        }

        Sprite SeasonSprite(BaseTurn turn)
        {
            if (turn is SpringTurn)
                return spring;
            else if (turn is SummerTurn)
                return summer;
            else if (turn is FallTurn)
                return fall;
            else if (turn is WinterTurn)
                return winter;
            else
                return null;
        }

    }
}

