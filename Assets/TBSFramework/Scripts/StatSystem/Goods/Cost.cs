using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Cost 
    {

        StatType type;
        public StatType Type { get { return type; } }

        Transaction value;
        public Transaction Value { get { return value; } }

        public Cost(StatType type, Transaction transaction)
        {
            this.type = type;
            this.value = transaction;
        }

        public Cost(StatType type, GoodType good, int amount, bool immediate, string description)
        {
            this.type = type;
            value = new Transaction(good, amount, immediate, description);
        }

        public int GetValueWithModifiers (IStatCollection<StatType> stats)
        {
            return Value.Good.Value + stats[Type];
        }

        public Transaction GetTransactionWithModifiers(IStatCollection<StatType> stats)
        {
            return new Transaction(Value.Good.Type, Value.Good.Value + stats[Type], Value.Immediate,Value.Description);
        }

        public static Cost ParseCSV(string[] csvLine, int startingIndex)
        {
            return new Cost(Enumeration.FromDisplayName<StatType>(csvLine[startingIndex]), (GoodType)Enum.Parse(typeof(GoodType), csvLine[startingIndex + 1]), Int32.Parse(csvLine[startingIndex + 2]), Boolean.Parse(csvLine[startingIndex + 3]), csvLine[startingIndex + 4]);
        }

        public static Cost[] ParseCSVList(string[] csvList)
        {
            List<Cost> items = new List<Cost>();
            for (int i = 0; i < csvList.Length; i += 5)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items.ToArray();
        }
    }
}
