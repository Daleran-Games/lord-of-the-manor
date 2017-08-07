using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class GoodCounter : MonoBehaviour
    {
        public GoodType TrackedGood = GoodType.Food;
        
        protected TextMeshProUGUI label;
        protected Group player;

        protected string posColor;
        protected string negColor;
    
        protected virtual void Start()
        {
            label = GetComponentInChildren<TextMeshProUGUI>();
            GameManager.Instance.Play.StateEnabled += OnGameStart;

            //Debug.Log(posColor);

            if (GameManager.Instance.CurrentState == GameManager.Instance.Play)
                OnGameStart(GameManager.Instance.CurrentState);
        }
        
        protected virtual void OnGameStart (GameState state)
        {
            posColor = ColorUtility.ToHtmlStringRGB(UIManager.Instance.Style.StatIncreaseColor);
            negColor = ColorUtility.ToHtmlStringRGB(UIManager.Instance.Style.StatDecreaseColor);

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
                label.text = player.Goods[TrackedGood] + " <color=#"+posColor+">(+" + nextTurn +")</color>";
            else if (nextTurn == 0)
                label.text = player.Goods[TrackedGood] + " (0)";
            else
                label.text = player.Goods[TrackedGood] + " <color=#" + negColor + ">(" + nextTurn + ")</color>";

            //Canvas.ForceUpdateCanvases();
        }

        protected virtual void OnDestroy()
        {
            player.Goods.GoodChanged -= OnGoodChanged;
            player.Goods.FutureTransactionsChanged -= OnPendingTransactionsChanged;
            GameManager.Instance.Play.StateEnabled -= OnGameStart;
        }
    }
}


