using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using UnityEngine.UI;
using DaleranGames.Database;

namespace DaleranGames.UI
{

    public class TileYieldOverlay : BaseOverlay
    {
        [SerializeField]
        string food1;
        TileGraphic food1g;
        [SerializeField]
        string food2;
        TileGraphic food2g;
        [SerializeField]
        string food3;
        TileGraphic food3g;
        [SerializeField]
        string wood1;
        TileGraphic wood1g;
        [SerializeField]
        string wood2;
        TileGraphic wood2g;
        [SerializeField]
        string wood3;
        TileGraphic wood3g;
        [SerializeField]
        string stone1;
        TileGraphic stone1g;
        [SerializeField]
        string stone2;
        TileGraphic stone2g;
        [SerializeField]
        string stone3;
        TileGraphic stone3g;
        [SerializeField]
        string gold1;
        TileGraphic gold1g;
        [SerializeField]
        string gold2;
        TileGraphic gold2g;
        [SerializeField]
        string gold3;
        TileGraphic gold3g;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        protected override void OnDatabaseInitialized()
        {
            base.OnDatabaseInitialized();
            food1g = GameDatabase.Instance.TileGraphics.Get(food1);
            food2g = GameDatabase.Instance.TileGraphics.Get(food2);
            food3g = GameDatabase.Instance.TileGraphics.Get(food3);
            wood1g = GameDatabase.Instance.TileGraphics.Get(wood1);
            wood2g = GameDatabase.Instance.TileGraphics.Get(wood2);
            wood3g = GameDatabase.Instance.TileGraphics.Get(wood3);
            stone1g = GameDatabase.Instance.TileGraphics.Get(stone1);
            stone2g = GameDatabase.Instance.TileGraphics.Get(stone2);
            stone3g = GameDatabase.Instance.TileGraphics.Get(stone3);
            gold1g = GameDatabase.Instance.TileGraphics.Get(gold1);
            gold2g = GameDatabase.Instance.TileGraphics.Get(gold2);
            gold3g = GameDatabase.Instance.TileGraphics.Get(gold3);

        }

        protected override void SetLabel(HexTile tile)
        {
            overlay.SetLabelIcon(tile, GoldGraphic(tile));
            overlay.SetDigit1(tile, FoodGraphic(tile));
            overlay.SetDigit2(tile, WoodGraphic(tile));
            overlay.SetDigit3(tile, StoneGraphic(tile));
        }

        TileGraphic FoodGraphic(HexTile tile)
        {
            switch (tile.Land.FoodYield.Value)
            {
                case 1:
                    return food1g;
                case 2:
                    return food2g;
                case 3:
                    return food3g;
                default:
                    return TileGraphic.clear;
            }
        }

        TileGraphic WoodGraphic(HexTile tile)
        {
            switch (tile.Land.WoodYield.Value)
            {
                case 1:
                    return wood1g;
                case 2:
                    return wood2g;
                case 3:
                    return wood3g;
                default:
                    return TileGraphic.clear;
            }
        }

        TileGraphic StoneGraphic(HexTile tile)
        {
            switch (tile.Land.StoneYield.Value)
            {
                case 1:
                    return stone1g;
                case 2:
                    return stone2g;
                case 3:
                    return stone3g;
                default:
                    return TileGraphic.clear;
            }
        }

        TileGraphic GoldGraphic(HexTile tile)
        {
            switch(tile.Land.GoldYield.Value)
            {
                case 1:
                    return gold1g;
                case 2:
                    return gold2g;
                case 3:
                    return gold3g;
                default:
                    return TileGraphic.clear;
            }
        }

    }
}
