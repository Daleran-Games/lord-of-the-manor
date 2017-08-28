using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DaleranGames.IO;
using DaleranGames.UI;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Modifier : IFormattable, IEquatable<Modifier>, IComparable<Modifier>, IComparable
    {
        [SerializeField]
        StatType type;
        public StatType Type { get { return type; } }

        [SerializeField]
        int value;
        public int Value { get { return value; } }
                
        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Modifier (StatType type, int value)
        {
            this.type = type;
            this.value = value;
            description = "None";
        }

        public Modifier(StatType type, int value, string description)
        {
            this.type = type;
            this.value = value;
            this.description = description;
        }

        public static Modifier ParseCSV(List<string> csvLine, int startingIndex)
        {
            return new Modifier(Enumeration.FromName<StatType>(csvLine[startingIndex]),Int32.Parse(csvLine[startingIndex+1]),csvLine[startingIndex+2]);
        }

        public static List<Modifier> ParseCSVList (List<string> csvList)
        {
            List<Modifier> items = new List<Modifier>();
            for (int i=0; i < csvList.Count; i+=3)
            {
                items.Add(ParseCSV(csvList, i));
            }
            return items;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1}) {2}", Type, Value, Description);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public string Info
        {
            get
            {
                if (Value > 0)
                    return Type.ToString() +": "+("+" + Value.ToString()).ToPositiveColor() + Type.Icon +" "+ Description.ToPositiveColor();
                else if (Value < 0)
                    return Type.ToString() + ": " + Value.ToString().ToNegativeColor() + Type.Icon +" "+ Description.ToNegativeColor();
                else
                    return Type.ToString() + ": " + Value.ToString() + Type.Icon + " " + Description;
            }
        }

        public bool Equals(Modifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Type == Type && other.Value == Value  && other.Description == Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Modifier) && Equals((Modifier)obj);
        }

        public int CompareTo(Modifier other)
        {
            if (other.Type == Type)
            {
                return Value.CompareTo(other.Value);
            }
            else
            {
                if (other.Type.Value > Type.Value)
                    return 1;
                else if (other.Type.Value == Type.Value)
                    return 0;
                else
                    return -1;
            }
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            if (obj.GetType() == typeof(Modifier) && Equals((Modifier)obj))
                return CompareTo((Modifier)obj);
            else
                throw new ArgumentException("Object is not a Modifer");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Description.GetHashCode()) ^ Type.GetHashCode();
            }
        }
        /*
        public static implicit operator int(Modifier m)
        {
            return m.Value;
        }

        public static implicit operator StatType(Modifier m)
        {
            return m.Type;
        }

        public static implicit operator Stat(Modifier m)
        {
            return new Stat(m.type, m.value);
        }
        */
    }
}
