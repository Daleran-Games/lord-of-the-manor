using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Cost 
    {
        [SerializeField]
        CostType modifiedBy;
        public CostType ModifiedBy { get { return modifiedBy; } }

        [SerializeField]
        int value;
        public int Value { get { return value; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public static readonly Cost Null = new Cost(CostType.NullCost, 0, "");

        public Cost(CostType modifiedBy, int amount, string description)
        {
            this.modifiedBy = modifiedBy;
            this.value = amount;
            this.description = description;
        }

        public int ModifiedValue (IStatCollection<StatType> stats)
        {
            return Value + stats[ModifiedBy];
        }

        public Transaction ModifiedTransaction(IStatCollection<StatType> stats)
        {
            return new Transaction(modifiedBy.Good, Value + stats[ModifiedBy],Description);
        }

        public static Cost ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Cost(Enumeration.FromDisplayName<CostType>(csvLine[startingIndex]), Int32.Parse(csvLine[startingIndex + 1]), csvLine[startingIndex + 2]);
        }

        public static List<Cost> ParseCSVList(List<string> csvList)
        {
            List<Cost> items = new List<Cost>();
            for (int i = 0; i < csvList.Count; i += 6)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items;
        }
    }
}
