using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.UI
{
    public class PopulationCounter : GoodCounter
    {
        protected override void Start()
        {
            TrackedGood = TBSFramework.GoodType.Population;
            base.Start();
        }

        protected override void UpdateLabel()
        {
            label.text = player.Goods[TrackedGood] + "/" + player.MaxPopulation.Value;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

    }
}