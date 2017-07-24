using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DaleranGames.TBSFramework;

namespace DaleranGames.UI
{
    public class PopulationCounter : GoodCounter
    {
        protected override void Start()
        {
            TrackedGood = GoodType.Population;
            base.Start();
        }

        protected override void OnGameStart(GameState state)
        {
            base.OnGameStart(state);
            player.Stats.StatModified += OnMaxPopChanged;
        }

        protected void OnMaxPopChanged(IStatCollection mods,StatType type)
        {


            if (type == StatType.MaxPopulation)
                UpdateLabel();
        }

        protected override void UpdateLabel()
        {
            if (player.Goods[TrackedGood] > player.MaxPopulation.Value || player.Goods[TrackedGood] <= 0)
                label.text = " <color=#" + negColor + ">"+player.Goods[TrackedGood] + "</color> / " + player.MaxPopulation.Value;
            else
                label.text = player.Goods[TrackedGood] + "/" + player.MaxPopulation.Value;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            player.Stats.StatModified -= OnMaxPopChanged;

        }

    }
}