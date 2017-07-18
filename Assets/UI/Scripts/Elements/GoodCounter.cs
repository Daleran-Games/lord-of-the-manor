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

        private void Start()
        {
            label = GetComponentInChildren<TextMeshProUGUI>();
            GameManager.Instance.Play.StateEnabled += OnGameStart;
        }
        
        protected virtual void OnGameStart (GameState state)
        {
            player = GroupManager.Instance.PlayerGroup;
            player.Goods.GoodChanged += OnGoodChanged;
            player.Goods.PendingTransactionsChanged += OnPendingTransactionsChanged;
            UpdateLabel();
            Debug.Log("Game start update labels");
        }

        protected virtual void OnPendingTransactionsChanged(GoodsCollection col, GoodType type)
        {
            UpdateLabel();
        }

        protected virtual void OnGoodChanged(GoodsCollection col, GoodType type)
        {
            UpdateLabel();
        }

        protected virtual void UpdateLabel()
        {

            label.text = player.Goods[TrackedGood] + " (" + player.Goods.GetAllPendingTransactionsOfType(TrackedGood).Value +")";
            Canvas.ForceUpdateCanvases();
        }

        private void OnDestroy()
        {
            player.Goods.GoodChanged -= OnGoodChanged;
            player.Goods.PendingTransactionsChanged -= OnPendingTransactionsChanged;
            GameManager.Instance.Play.StateEnabled -= OnGameStart;
        }
    }
}


