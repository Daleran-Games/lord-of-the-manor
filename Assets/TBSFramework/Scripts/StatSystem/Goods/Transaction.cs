using System.Collections;
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
        bool immediate;
        public bool Immediate { get { return immediate; } }

        [SerializeField]
        Good good;
        public Good Good { get { return good; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Transaction(Good good, bool immediate)
        {
            this.good = good;
            this.immediate = immediate;
            description = "None";
        }

        public Transaction(Good good, bool immediate, string description)
        {
            this.good = good;
            this.immediate = immediate;
            this.description = description;
        }

        public Transaction(GoodType type, int amount, bool immediate, string description)
        {
            this.good = new Good(type, amount);
            this.immediate = immediate;
            this.description = description;
        }

        public static Transaction ParseCSV(string[] csvLine, int startingIndex)
        {
            return new Transaction(new Good((GoodType)Enum.Parse(typeof(GoodType), csvLine[startingIndex]), Int32.Parse(csvLine[startingIndex + 1])),Boolean.Parse(csvLine[startingIndex + 2]), csvLine[startingIndex + 3]);
        }

        public static Transaction[] ParseCSVList(string[] csvList)
        {
            List<Transaction> items = new List<Transaction>();
            for (int i = 0; i < csvList.Length; i += 4)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items.ToArray();
        }

        public override string ToString()
        {
            if (Immediate)
                return string.Format("{0} {1}, Immediate", Good,Description);
            else
                return string.Format("{0} {1}, End Turn", Good, Description);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Transaction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Good == Good && other.Immediate == Immediate && other.Description == Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Transaction) && Equals((Transaction)obj);
        }

        public int CompareTo(Transaction other)
        {
            if (other.Good > Good)
                return 1;
            else if (other.Equals(this))
                return 0;
            else
                return -1;
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
                return (Description.GetHashCode()) ^ Good.Type.GetHashCode();
            }
        }

        public static implicit operator int(Transaction t)
        {
            return t.Good.Value;
        }

        public static implicit operator GoodType(Transaction t)
        {
            return t.Good.Type;
        }

        public static implicit operator Good(Transaction t)
        {
            return t.Good;
        }
    }
}