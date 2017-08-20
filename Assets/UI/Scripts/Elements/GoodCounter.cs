using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DaleranGames.TBSFramework;
using UnityEngine.EventSystems;
using System;

namespace DaleranGames.UI
{
    public class GoodCounter : MonoBehaviour, ITooltipableGameObject
    {
        public GoodType TrackedGood = GoodType.Food;
        
        [SerializeField]
        protected TextMeshProUGUI label;
        protected Group player;

        protected virtual void Start()
        {
            GameManager.Instance.Play.StateEnabled += OnGameStart;

            //Debug.Log(posColor);

            if (GameManager.Instance.CurrentState == GameManager.Instance.Play)
                OnGameStart(GameManager.Instance.CurrentState);
        }
        
        protected virtual void OnGameStart (GameState state)
        {
            player = GroupManager.Instance.PlayerGroup;
            player.Goods.GoodChanged += OnGoodChanged;
            player.Goods.FutureTransactionsChanged += OnPendingTransactionsChanged;
            UpdateLabel();
            //Debug.Log("Game start update labels");
        }

        protected virtual void OnPendingTransactionsChanged(GoodsCollection col, GoodType type)
        {
            if (type == TrackedGood)
                UpdateLabel();
        }

        protected virtual void OnGoodChanged(GoodsCollection col, GoodType type)
        {
            if (type == TrackedGood)
                UpdateLabel();
        }

        protected virtual void UpdateLabel()
        {
            int nextTurn = player.Goods.GetTotalPendingTransactionsOfType(TrackedGood).Value;

            label.text = player.Goods[TrackedGood] + " ("+TextUtilities.ColorBasedOnNumber(nextTurn.ToString(),nextTurn,true)+")";
            //Canvas.ForceUpdateCanvases();
        }

        protected virtual void OnDestroy()
        {
            player.Goods.GoodChanged -= OnGoodChanged;
            player.Goods.FutureTransactionsChanged -= OnPendingTransactionsChanged;
            GameManager.Instance.Play.StateEnabled -= OnGameStart;
        }

        protected virtual string GenerateTooltipText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append((TrackedGood.ToString()+TextUtilities.GetGoodTypeIcon(TrackedGood)).ToHeaderStyle());
            sb.Append(" "+ player.Goods[TrackedGood]);

            if (TrackedGood == GoodType.Food)
                sb.AppendLine("/"+player.Stats[StatType.MaxFood]);
            else if (TrackedGood == GoodType.Population)
                sb.AppendLine("/" + player.Stats[StatType.MaxPopulation]);
            else
                sb.AppendLine();

            List<Transaction> current = player.Goods.GetAllCurrentTransactionsOfType(TrackedGood, true);

            if (current.Count > 0)
            {
                sb.AppendLine("<u>This Turn</u>");
                for (int i = 0; i < current.Count; i++)
                {
                    sb.AppendLine(TextUtilities.ColorBasedOnNumber(current[i].Value.ToString(), current[i].Value, true) + " " + current[i].Description);
                }
            }

            List<Transaction> pending = player.Goods.GetAllPendingTransactionsOfType(TrackedGood, true);

            if (pending.Count > 0)
            {
                sb.AppendLine("<u>Next Turn</u>");
                for (int i = 0; i < pending.Count; i++)
                {
                    sb.AppendLine(TextUtilities.ColorBasedOnNumber(pending[i].Value.ToString(), pending[i].Value, true) + " " + pending[i].Description);
                }
            }

            sb.AppendLine(TextUtilities.GetGoodTypeDescription(TrackedGood).ToFootnoteStyle());

            return sb.ToString();
        }

        public virtual string Info { get { return GenerateTooltipText(); } }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if(eventData.pointerEnter == this.gameObject)
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


