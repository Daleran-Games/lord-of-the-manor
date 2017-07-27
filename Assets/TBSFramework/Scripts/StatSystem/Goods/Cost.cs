using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Cost 
    {

        CostType modifiedBy;
        public CostType ModifiedBy { get { return modifiedBy; } }

        [SerializeField]
        int value;
        public int Value { get { return value; } }

        [SerializeField]
        bool immediate;
        public bool Immediate { get { return immediate; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Cost(CostType modifiedBy, int amount, bool immediate, string description)
        {
            this.modifiedBy = modifiedBy;
            this.value = amount;
            this.immediate = immediate;
            this.description = description;
        }

        public int GetValueWithModifiers (IStatCollection<StatType> stats)
        {
            return Value + stats[ModifiedBy];
        }

        public Transaction GetTransactionWithModifiers(IStatCollection<StatType> stats)
        {
            return new Transaction(modifiedBy.Good, Value + stats[ModifiedBy], Immediate,Description);
        }

        public static Cost ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Cost(Enumeration.FromDisplayName<CostType>(csvLine[startingIndex]), Int32.Parse(csvLine[startingIndex + 1]), Boolean.Parse(csvLine[startingIndex + 2]), csvLine[startingIndex + 3]);
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
