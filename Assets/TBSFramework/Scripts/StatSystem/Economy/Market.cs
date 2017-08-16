using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    public class Market : Singleton<Market>
    {
        protected Market() { }

        public string MarketName = "Bebbanburg";

        [Header("Market Settings")]
        public int BaseLaborCostPerStack = 1;
        public int BaseTaxAmountPerStack = 1;
        public int StackSize = 10;

        [Header("Spring Prices")]
        public SeasonPrices SpringPrices;
        [Header("Summer Prices")]
        public SeasonPrices SummerPrices;
        [Header("Fall Prices")]
        public SeasonPrices FallPrices;
        [Header("Winter Prices")]
        public SeasonPrices WinterPrices;

        [Header("Current Prices")]
        public SeasonPrices CurrentPrices;

        private void Start()
        {
            TurnManager.Instance.TurnStart += SetTurnPrices;
            SetTurnPrices(TurnManager.Instance.CurrentTurn);
        }

        protected override void OnDestroy()
        {
            TurnManager.Instance.TurnStart -= SetTurnPrices;
        }

        [System.Serializable]
        public class SeasonPrices
        {
            public int FoodSellPrice;
            public int FoodBuyPrice;
            public int FoodQuantitiy;

            public int WoodSellPrice;
            public int WoodBuyPrice;
            public int WoodQuantitiy;

            public int StoneSellPrice;
            public int StoneBuyPrice;
            public int StoneQuantity;

            public int LaborBuyPrice;
            public int LaborQuantitiy;
        }

        public bool CanBuy(GoodType type, Group group)
        {
            switch (type)
            {
                case GoodType.Food:
                    return Food.Value;
                case GoodType.Wood:
                    return Wood.Value;
                case GoodType.Stone:
                    return Stone.Value;
                case GoodType.Gold:
                    return Gold.Value;
                case GoodType.Population:
                    return Population.Value;
                case GoodType.Labor:
                    return Labor.Value;
                default:
                    return false;
            }
        }

        public void Buy (GoodType type, Group group)
        {
            switch (type)
            {
                case GoodType.Food:
                    food = value;
                    OnGoodChanged(this, Food.Type);
                    break;
                case GoodType.Wood:
                    wood = value;
                    OnGoodChanged(this, Wood.Type);
                    break;
                case GoodType.Stone:
                    stone = value;
                    OnGoodChanged(this, Stone.Type);
                    break;
                case GoodType.Gold:
                    gold = value;
                    OnGoodChanged(this, Gold.Type);
                    break;
                case GoodType.Population:
                    population = value;
                    OnGoodChanged(this, Population.Type);
                    break;
                case GoodType.Labor:
                    labor = value;
                    OnGoodChanged(this, Labor.Type);
                    break;
            }
        }

        public bool CanSell(GoodType type, Group group)
        {
            return false;
        }

        public void Sell(GoodType type, Group group)
        {

        }

        public void SetTurnPrices (BaseTurn turn)
        {
            if (turn is SpringTurn)
                CurrentPrices = SpringPrices;
            else if (turn is SummerTurn)
                CurrentPrices = SummerPrices;
            else if (turn is FallTurn)
                CurrentPrices = FallPrices;
            else if (turn is WinterTurn)
                CurrentPrices = WinterPrices;
        }
        
        
    }
}