using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Cost 
    {

        StatType modifiedBy;
        public StatType ModifiedBy { get { return modifiedBy; } }

        [SerializeField]
        GoodType type;
        public GoodType Type { get { return type; } }

        [SerializeField]
        int value;
        public int Value { get { return value; } }

        [SerializeField]
        bool immediate;
        public bool Immediate { get { return immediate; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Cost(StatType modifiedBy, GoodType type, int amount, bool immediate, string description)
        {
            this.modifiedBy = modifiedBy;
            this.type = type;
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
            return new Transaction(Type, Value + stats[ModifiedBy], Immediate,Description);
        }

        public static Cost ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Cost(Enumeration.FromDisplayName<StatType>(csvLine[startingIndex]), (GoodType)Enum.Parse(typeof(GoodType), csvLine[startingIndex + 1]), Int32.Parse(csvLine[startingIndex + 2]), Boolean.Parse(csvLine[startingIndex + 3]), csvLine[startingIndex + 4]);
        }

        public static List<Cost> ParseCSVList(List<string> csvList)
        {
            List<Cost> items = new List<Cost>();
            for (int i = 0; i < csvList.Count; i += 5)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items;
        }
    }
}
