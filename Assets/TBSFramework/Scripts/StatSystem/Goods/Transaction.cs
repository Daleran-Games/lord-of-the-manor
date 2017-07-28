﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Transaction : IFormattable, IEquatable<Transaction>, IComparable<Transaction>, IComparable
    {
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

        public Transaction(GoodType type, int amount, bool immediate)
        {
            this.type = type;
            this.value = amount;
            this.immediate = immediate;
            description = "None";
        }

        public Transaction(GoodType type, int amount, bool immediate, string description)
        {
            this.type = type;
            this.value = amount;
            this.immediate = immediate;
            this.description = description;
        }

        public static Transaction ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Transaction((GoodType)Enum.Parse(typeof(GoodType), csvLine[startingIndex]), Int32.Parse(csvLine[startingIndex + 1]),Boolean.Parse(csvLine[startingIndex + 2]), csvLine[startingIndex + 3]);
        }

        public static List<Transaction> ParseCSVList(List<string> csvList)
        {
            List<Transaction> items = new List<Transaction>();
            for (int i = 0; i < csvList.Count; i += 4)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items;
        }

        public override string ToString()
        {
            if (Immediate)
                return string.Format("{0} ({1}) {2}, Immediate", type,value, description);
            else
                return string.Format("{0} ({1}) {2}, End Turn", type, value, description);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Transaction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.type == type && other.value == value && other.Description == Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Transaction) && Equals((Transaction)obj);
        }

        public int CompareTo(Transaction other)
        {
            if (other.Type == Type)
            {
                return value.CompareTo(other.value);
            }
            else
            {
                if (other.Type > Type)
                    return 1;
                else if (other.Type == Type)
                    return 0;
                else
                    return -1;
            }
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            if (obj.GetType() == typeof(Transaction) && Equals((Transaction)obj))
                return CompareTo((Transaction)obj);
            else
                throw new ArgumentException("Object is not a Transaction");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Description.GetHashCode()) ^ Type.GetHashCode();
            }
        }

        public static implicit operator int(Transaction t)
        {
            return t.Value;
        }

        public static implicit operator GoodType(Transaction t)
        {
            return t.Type;
        }

        public static implicit operator Good(Transaction t)
        {
            return new Good(t.Type, t.Value);
        }
    }
}