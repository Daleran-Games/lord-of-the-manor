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

        protected void OnMaxPopChanged(IStatCollection<StatType> mods,StatType type)
        {
            if (type == StatType.MaxPopulation)
                UpdateLabel();
        }

        protected override void UpdateLabel()
        {
            if (player.Goods[TrackedGood] > player.Stats[StatType.MaxPopulation] || player.Goods[TrackedGood] <= 0)
                label.text = player.Goods[TrackedGood].ToString().ToNegativeColor() + " / " + player.Stats[StatType.MaxPopulation];
            else
                label.text = player.Goods[TrackedGood] + "/" + player.Stats[StatType.MaxPopulation];
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            player.Stats.StatModified -= OnMaxPopChanged;

        }

    }
}