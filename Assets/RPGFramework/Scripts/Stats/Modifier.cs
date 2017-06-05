using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class Modifier
    {

        public Modifier (StatType statEffected, int amount)
        {
            Amount = amount;
            StatEffected = statEffected;
        }

        [SerializeField]
        protected int amount;
        public int Amount { get; protected set; }

        [SerializeField]
        protected StatType statEffected;
        public StatType StatEffected { get; protected set; }


    }
}
