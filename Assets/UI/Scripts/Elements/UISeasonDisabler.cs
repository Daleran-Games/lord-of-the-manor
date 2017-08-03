using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class UISeasonDisabler : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        bool spring = true;

        [SerializeField]
        bool summer = true;

        [SerializeField]
        bool fall = true;

        [SerializeField]
        bool winter = true;


        // Use this for initialization
        void Start()
        {
            TurnManager.Instance.TurnStart += OnSeasonStart;
        }

        void OnSeasonStart(BaseTurn turn)
        {
            if (turn == TurnManager.Instance.Spring && spring == false)
                button.interactable = false;
            else if (turn == TurnManager.Instance.Summer && summer == false)
                button.interactable = false;
            else if (turn == TurnManager.Instance.Fall && fall == false)
                button.interactable = false;
            else if (turn == TurnManager.Instance.Winter && winter == false)
                button.interactable = false;
            else
                button.interactable = true;
        }

        private void OnDestroy()
        {
            TurnManager.Instance.TurnStart -= OnSeasonStart;
        }

    }
}

