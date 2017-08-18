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
            int nextTurn = player.Goods.GetAllPendingTransactionsOfType(TrackedGood).Value;

            if (nextTurn > 0)
                label.text = player.Goods[TrackedGood] + " <style=\"PosColor\">(+" + nextTurn +")</style>";
            else if (nextTurn == 0)
                label.text = player.Goods[TrackedGood] + " (0)";
            else
                label.text = player.Goods[TrackedGood] + " <style=\"NegColor\">(" + nextTurn + ")</style>";

            OnInfoUpdate(ObjectInfo);
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
            return "";
        }

        public virtual string ObjectInfo { get { return GenerateTooltipText(); } }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            TooltipManager.Instance.ShowTooltip(ObjectInfo);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            TooltipManager.Instance.HideTooltip();
        }

        public void OnInfoUpdate(string newInfo)
        {
            TooltipManager.Instance.UpdateText(newInfo);
        }
    }
}


