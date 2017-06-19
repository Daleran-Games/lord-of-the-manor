using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Resource
    {
        public enum ResourceType
        {
            Food,
            Wood,
            Stone,
            Coins,
            Work
        }
        [SerializeField]
        ResourceType type;
        public ResourceType Type { get { return type; } set { type = value; } }

        [SerializeField]
        int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                if (value < 0)
                    amount = 0;
                else
                    amount = value;


                if (AmountChanged != null)
                    AmountChanged(Amount);
            }
        }
        public Action<int> AmountChanged;

        [SerializeField]
        int max;

        [SerializeField]
        int rate;

        
        [System.Serializable]
        public class ResourceModifier
        {
            [SerializeField]
            ResourceType type;
            public ResourceType Type { get { return type; } set { type = value; } }

            [SerializeField]
            int amount;
            public int Amount { get { return amount; } set { amount = value; } }

            [SerializeField]
            string description;
            public string Description { get { return description; } set { description = value; } }

            public ResourceModifier(ResourceType type, int amt, string desc)
            {
                Type = type;
                Amount = amt;
                Description = desc;
            }
        }
    }
}

