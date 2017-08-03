﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Cost
    {
        [SerializeField]
        GoodType good;
        public GoodType Good { get { return good; } }

        [SerializeField]
        StatType modifiedBy;
        public StatType ModifiedBy { get { return modifiedBy; } }

        [SerializeField]
        int value;
        public int Value { get { return value; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public static readonly Cost Null = new Cost(GoodType.None,StatType.NullStat, 0, "");

        public Cost(GoodType good, StatType modifiedBy, int amount, string description)
        {
            this.good = good;
            this.modifiedBy = modifiedBy;
            this.value = amount;
            this.description = description;
        }

        public int ModifiedValue(IStatCollection<StatType> stats)
        {
            return Value + stats[ModifiedBy];
        }

        public Transaction ModifiedTransaction(IStatCollection<StatType> stats)
        {
            return new Transaction(good, Value + stats[ModifiedBy], Description);
        }

        public static Cost ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Cost((GoodType)Enum.Parse(typeof(GoodType), csvLine[startingIndex]), Enumeration.FromDisplayName<StatType>(csvLine[startingIndex]), Int32.Parse(csvLine[startingIndex + 1]), csvLine[startingIndex + 2]);
        }

        public static List<Cost> ParseCSVList(List<string> csvList)
        {
            List<Cost> items = new List<Cost>();
            for (int i = 0; i < csvList.Count; i += 4)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items;
        }
    }
}