using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.RPGFramework
{
    [System.Serializable]
    public class Resource
    {
        public enum ResourceType
        {

        }

        [SerializeField]
        ResourceType type;
        public ResourceType Type { get { return type; } set { type = value; } }

        [SerializeField]
        int amount;
        public int Amount { get { return amount; } set { amount = value; } }
        public Action<int> AmountChanged;

        [SerializeField]
        int max;

        
        [System.Serializable]
        public class ResourceMod
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

            public ResourceMod(ResourceType type, int amt, string desc)
            {
                Type = type;
                Amount = amt;
                Description = desc;
            }

        }
    }
}

