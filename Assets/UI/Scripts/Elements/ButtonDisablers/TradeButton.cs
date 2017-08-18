using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class TradeButton : MonoBehaviour
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
            if (type == goodType || type == GoodType.Gold || type == GoodType.Labor)
            {
                CheckConditions();
            }

        }

        void CheckConditions()
        {
            if (buy)
            {
                if (Market.Instance.CanBuy(goodType,GroupManager.Instance.PlayerGroup))
                    button.interactable = true;
                else
                    button.interactable = false;
            } else
            {
                if (Market.Instance.CanSell(goodType, GroupManager.Instance.PlayerGroup))
                    button.interactable = true;
                else
                    button.interactable = false;
            }
        }

        private void OnDestroy()
        {
            TurnManager.Instance.TurnStart -= OnSeasonStart;
            GroupManager.Instance.PlayerGroup.Goods.GoodChanged -= OnValueChange;
        }
    }
}
