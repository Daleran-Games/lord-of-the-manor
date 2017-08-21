using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class TradeButton : MonoBehaviour, ITooltipableGameObject
    {
        [SerializeField]
        Button button;

        public int GoodTypeID=0;
        GoodType goodType;

        [SerializeField]
        bool buy;


        void Start()
        {
            goodType = GoodType.FromValue<GoodType>(GoodTypeID);
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

        protected virtual string GenerateTooltipText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if(buy)
            {
                sb.AppendLine(TextUtilities.ToHeaderStyle("Buy " + goodType));
                sb.AppendLine("Buy <b>" + Market.Instance.StackSize + "</b> " + goodType.Icon + " for " +Market.Instance.GetGoldCost(goodType).ToString().ToNegativeColor()+ GoodType.Gold.Icon + " and "+Market.Instance.GetLaborCost(goodType).ToString().ToNegativeColor() + GoodType.Labor.Icon + ".");

                if (!Market.Instance.CanBuy(goodType, GroupManager.Instance.PlayerGroup))
                    sb.AppendLine("You cannot afford to buy this.".ToNegativeColor());
            }
            else
            {
                sb.AppendLine(TextUtilities.ToHeaderStyle("Sell " + goodType));
                sb.AppendLine("Sell <b>" + Market.Instance.StackSize + "</b> " + goodType.Icon + " for " + Market.Instance.GetSellPrice(goodType).ToString().ToPositiveColor() + GoodType.Gold.Icon + " and " + Market.Instance.GetLaborCost(goodType).ToString().ToNegativeColor() + GoodType.Labor.Icon + ".");

                if (!Market.Instance.CanSell(goodType, GroupManager.Instance.PlayerGroup))
                    sb.AppendLine("You cannot sell this.".ToNegativeColor());

            }
                
            return sb.ToString();
        }

        public virtual string Info { get { return GenerateTooltipText(); } }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
                TooltipManager.Instance.ShowTooltip(Info);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }

        public void OnInfoUpdate(string newInfo)
        {
            //TooltipManager.Instance.UpdateText(newInfo);
        }
    }
}
