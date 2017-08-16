using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class TradeButtonDisabler : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        GoodType goodType;

        [SerializeField]
        bool buy;


        void Start()
        {
            TurnManager.Instance.TurnStart += OnSeasonStart;
            GroupManager.Instance.PlayerGroup.Goods.GoodChanged += OnValueChange;
            OnSeasonStart(TurnManager.Instance.CurrentTurn);
        }

        void OnSeasonStart(BaseTurn turn)
        {
            CheckConditions();
        }

        void OnValueChange(GoodsCollection goods, GoodType type)
        {
            if (type == goodType)
            {
                CheckConditions();
            }

        }

        void CheckConditions()
        {
            if (GroupManager.Instance.PlayerGroup.Goods[goodType] < threshold)
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
