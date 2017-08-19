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
                    return (group.Goods[GoodType.Gold] >= ((CurrentPrices.FoodBuyPrice * StackSize) + BaseTaxAmountPerStack) && group.Goods[GoodType.Labor] >= BaseLaborCostPerStack * GameplayMetrics.FoodLaborMultiplier);
                case GoodType.Wood:
                    return (group.Goods[GoodType.Gold] >= ((CurrentPrices.WoodBuyPrice * StackSize) + BaseTaxAmountPerStack) && group.Goods[GoodType.Labor] >= BaseLaborCostPerStack * GameplayMetrics.WoodLaborMultiplier);
                case GoodType.Stone:
                    return (group.Goods[GoodType.Gold] >= ((CurrentPrices.StoneBuyPrice * StackSize) + BaseTaxAmountPerStack) && group.Goods[GoodType.Labor] >= BaseLaborCostPerStack * GameplayMetrics.StoneLaborMultiplier);
                case GoodType.Labor:
                    return (group.Goods[GoodType.Gold] >= (CurrentPrices.LaborBuyPrice * StackSize));
                default:
                    return false;
            }
        }

        public int GetGoldCost(GoodType type)
        {
            switch (type)
            {
                case GoodType.Food:
                    return ((CurrentPrices.FoodBuyPrice * StackSize) + BaseTaxAmountPerStack);
                case GoodType.Wood:
                    return ((CurrentPrices.WoodBuyPrice * StackSize) + BaseTaxAmountPerStack);
                case GoodType.Stone:
                    return ((CurrentPrices.StoneBuyPrice * StackSize) + BaseTaxAmountPerStack);
                case GoodType.Labor:
                    return  (CurrentPrices.LaborBuyPrice * StackSize);
                default:
                    return 0;
            }
        }

        public int GetLaborCost(GoodType type)
        {
            switch (type)
            {
                case GoodType.Food:
                    return BaseLaborCostPerStack * GameplayMetrics.FoodLaborMultiplier;
                case GoodType.Wood:
                    return BaseLaborCostPerStack * GameplayMetrics.WoodLaborMultiplier;
                case GoodType.Stone:
                    return BaseLaborCostPerStack * GameplayMetrics.StoneLaborMultiplier;
                default:
                    return 0;
            }
        }

        public void Buy (GoodType type, Group group)
        {
            switch (type)
            {
                case GoodType.Food:
                    group.Goods.ProcessNow(new Transaction(GoodType.Food, StackSize, "Bought"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -(CurrentPrices.FoodBuyPrice * StackSize), "Purchased Food"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -BaseTaxAmountPerStack, "Taxes"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, -BaseLaborCostPerStack * GameplayMetrics.FoodLaborMultiplier, "Transporting Goods"));
                    break;
                case GoodType.Wood:
                    group.Goods.ProcessNow(new Transaction(GoodType.Wood, StackSize, "Bought"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -(CurrentPrices.WoodBuyPrice * StackSize), "Purchased Wood"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -BaseTaxAmountPerStack, "Taxes"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, -BaseLaborCostPerStack * GameplayMetrics.WoodLaborMultiplier, "Transporting Goods"));
                    break;
                case GoodType.Stone:
                    group.Goods.ProcessNow(new Transaction(GoodType.Stone, StackSize, "Bought"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -(CurrentPrices.StoneBuyPrice * StackSize), "Purchased Stone"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -BaseTaxAmountPerStack, "Taxes"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, -BaseLaborCostPerStack * GameplayMetrics.StoneLaborMultiplier, "Transporting Goods"));
                    break;
                case GoodType.Labor:
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, StackSize, "Bought"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, -(CurrentPrices.FoodBuyPrice * StackSize), "Purchased Labor"));
                    break;
            }
        }

        public bool CanSell(GoodType type, Group group)
        {
            switch (type)
            {
                case GoodType.Food:
                    return (group.Goods[GoodType.Food] >= StackSize && group.Goods[GoodType.Labor] >= BaseLaborCostPerStack * GameplayMetrics.FoodLaborMultiplier);
                case GoodType.Wood:
                    return (group.Goods[GoodType.Wood] >= StackSize && group.Goods[GoodType.Labor] >= BaseLaborCostPerStack * GameplayMetrics.WoodLaborMultiplier);
                case GoodType.Stone:
                    return (group.Goods[GoodType.Stone] >= StackSize && group.Goods[GoodType.Labor] >= BaseLaborCostPerStack * GameplayMetrics.StoneLaborMultiplier);
                default:
                    return false;
            }
        }

        public int GetSellPrice (GoodType type)
        {
            switch (type)
            {
                case GoodType.Food:
                    return (CurrentPrices.FoodSellPrice * StackSize);
                case GoodType.Wood:
                    return (CurrentPrices.WoodSellPrice * StackSize);
                case GoodType.Stone:
                    return (CurrentPrices.StoneSellPrice * StackSize);
                default:
                    return 0;
            }
        }

        public void Sell(GoodType type, Group group)
        {
            switch (type)
            {
                case GoodType.Food:
                    group.Goods.ProcessNow(new Transaction(GoodType.Food, -StackSize, "Sold"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, (CurrentPrices.FoodSellPrice * StackSize), "Sold Food"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, -BaseLaborCostPerStack * GameplayMetrics.FoodLaborMultiplier, "Transporting Goods"));
                    break;
                case GoodType.Wood:
                    group.Goods.ProcessNow(new Transaction(GoodType.Wood, -StackSize, "Sold"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, (CurrentPrices.WoodSellPrice * StackSize), "Sold Wood"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, -BaseLaborCostPerStack * GameplayMetrics.WoodLaborMultiplier, "Transporting Goods"));
                    break;
                case GoodType.Stone:
                    group.Goods.ProcessNow(new Transaction(GoodType.Stone, -StackSize, "Sold"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Gold, (CurrentPrices.StoneSellPrice * StackSize), "Sold Stone"));
                    group.Goods.ProcessNow(new Transaction(GoodType.Labor, -BaseLaborCostPerStack * GameplayMetrics.StoneLaborMultiplier, "Transporting Goods"));
                    break;
            }
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