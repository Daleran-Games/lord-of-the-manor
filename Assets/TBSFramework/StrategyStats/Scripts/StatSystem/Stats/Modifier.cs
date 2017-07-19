using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Modifier : IFormattable, IEquatable<Modifier>, IComparable<Modifier>, IComparable
    {
        public const string CsvIdentifier = "modifier";

        [SerializeField]
        Stat stat;
        public Stat Stat { get { return stat; } }

        [SerializeField]
        string description;
        public string Description { get { return description; } }

        public Modifier (Stat mod)
        {
            this.stat = mod;
            description = "None";
        }

        public Modifier(Stat mod, string description)
        {
            this.stat = mod;
            this.description = description;
        }

        public Modifier ParseCSV(string[] csvLine, int startingIndex)
        {
            return new Modifier(new Stat(Enumeration.FromDisplayName<StatType>(csvLine[startingIndex]),Int32.Parse(csvLine[startingIndex+1])),csvLine[startingIndex+2]);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Stat, Description);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Modifier other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Stat == Stat  && other.Description == Description;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Modifier) && Equals((Modifier)obj);
        }

        public int CompareTo(Modifier other)
        {
            if (other.Stat > Stat)
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

            if (obj.GetType() == typeof(Modifier) && Equals((Modifier)obj))
                return CompareTo((Modifier)obj);
            else
                throw new ArgumentException("Object is not a Modifer");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Description.GetHashCode()) ^ Stat.Type.GetHashCode();
            }
        }

        public static implicit operator int(Modifier m)
        {
            return m.Stat.Value;
        }

        public static implicit operator StatType(Modifier m)
        {
            return m.Stat.Type;
        }

        public static implicit operator Stat(Modifier m)
        {
            return m.Stat;
        }

    }
}
