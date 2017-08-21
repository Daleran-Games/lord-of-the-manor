using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DaleranGames.IO;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Transaction : IFormattable, IEquatable<Transaction>, IComparable<Transaction>, IComparable
    {
        [SerializeField]
        GoodType type;
        public GoodType Type { get { return type; } }

        [SerializeField]
        int value;
        public int Value { get { return value; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }


        public Transaction(GoodType type, int amount, string description)
        {
            this.type = type;
            this.value = amount;
            this.description = description;
        }

        public static Transaction ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Transaction(Enumeration.FromName<GoodType>(csvLine[startingIndex]), Int32.Parse(csvLine[startingIndex + 1]), csvLine[startingIndex + 2]);
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
            return string.Format("{0} ({1}) {2}", type, value, description);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Transaction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Type == Type && other.Value == Value && other.Description == Description;
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

        /*
        public static explicit operator int(Transaction t)
        {
            return t.Value;
        }

        public static explicit operator GoodType(Transaction t)
        {
            return t.Type;
        }

        public static explicit operator Good(Transaction t)
        {
            return new Good(t.Type, t.Value);
        }
        */
    }
}