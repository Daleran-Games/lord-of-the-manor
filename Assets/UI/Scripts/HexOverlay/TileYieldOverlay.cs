﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;
using UnityEngine.UI;
using DaleranGames.IO;

namespace DaleranGames.UI
{

    public class TileYieldOverlay : BaseOverlay
    {
#pragma warning disable 0649
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
#pragma warning restore 0649
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            food1g = GameDatabase.Instance.TileGraphics[food1];
            food2g = GameDatabase.Instance.TileGraphics[food2];
            food3g = GameDatabase.Instance.TileGraphics[food3];
            wood1g = GameDatabase.Instance.TileGraphics[wood1];
            wood2g = GameDatabase.Instance.TileGraphics[wood2];
            wood3g = GameDatabase.Instance.TileGraphics[wood3];
            stone1g = GameDatabase.Instance.TileGraphics[stone1];
            stone2g = GameDatabase.Instance.TileGraphics[stone2];
            stone3g = GameDatabase.Instance.TileGraphics[stone3];
            gold1g = GameDatabase.Instance.TileGraphics[gold1];
            gold2g = GameDatabase.Instance.TileGraphics[gold2];
            gold3g = GameDatabase.Instance.TileGraphics[gold3];
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
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
            switch (tile.Stats[StatType.FoodYield])
            {
                case 1:
                    return food1g;
                case 2:
                    return food2g;
                case 3:
                    return food3g;
                default:
                    return TileGraphic.Clear;
            }
        }

        TileGraphic WoodGraphic(HexTile tile)
        {
            switch (tile.Stats[StatType.WoodYield])
            {
                case 1:
                    return wood1g;
                case 2:
                    return wood2g;
                case 3:
                    return wood3g;
                default:
                    return TileGraphic.Clear;
            }
        }

        TileGraphic StoneGraphic(HexTile tile)
        {
            switch (tile.Stats[StatType.StoneYield])
            {
                case 1:
                    return stone1g;
                case 2:
                    return stone2g;
                case 3:
                    return stone3g;
                default:
                    return TileGraphic.Clear;
            }
        }

        TileGraphic GoldGraphic(HexTile tile)
        {
            switch(tile.Stats[StatType.GoldYield])
            {
                case 1:
                    return gold1g;
                case 2:
                    return gold2g;
                case 3:
                    return gold3g;
                default:
                    return TileGraphic.Clear;
            }
        }

    }
}
