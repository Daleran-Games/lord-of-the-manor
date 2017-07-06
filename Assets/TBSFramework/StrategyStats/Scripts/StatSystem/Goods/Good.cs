using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public class Good : IFormattable, IEquatable<Good>, IComparable<Good>, IComparable
    {

        public enum Category
        {
            Food,
            Wood,
            Gold,
            Stone,
            Work,
            Population
        }

        [SerializeField]
        Category _type;
        [SerializeField]
        int _value;

        public Category Type { get { return _type; } }
        public int Value { get { return _value; } }

        public Good(Category type, int val)
        {
            _type = type;
            _value = val;
        }

        public override string ToString()
        {
            return string.Format("({0}: {1})", Type, Value);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Good other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Type == Type && other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Good) && Equals((Good)obj);
        }

        public int CompareTo(Good other)
        {
            if (other.Value > Value)
                return 1;
            else if (other.Value == Value)
                return 0;
            else
                return -1;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            if (obj.GetType() == typeof(Good) && Equals((Good)obj))
                return CompareTo((Good)obj);
            else
                throw new ArgumentException("Object is not a Good");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode()) ^ Type.GetHashCode();
            }
        }

        public Good ToType(Category t)
        {
            return new Good(t, Value);
        }

        public static bool operator ==(Good l, Good r)
        {
            return Equals(l, r);
        }

        public static bool operator ==(Good l, int r)
        {
            return l.Value == r;
        }

        public static bool operator !=(Good l, Good r)
        {
            return !Equals(l, r);
        }

        public static bool operator !=(Good l, int r)
        {
            return !(l.Value == r);
        }

        public static Good operator +(Good l, int r)
        {
            return new Good(l.Type, l.Value + r);
        }

        public static Good operator -(Good l, int r)
        {
            return new Good(l.Type, l.Value - r);
        }

        public static Good operator *(Good l, int r)
        {
            return new Good(l.Type, l.Value * r);
        }

        public static Good operator /(Good l, int r)
        {
            return new Good(l.Type, l.Value / r);
        }

        public static Good operator %(Good l, int r)
        {
            return new Good(l.Type, l.Value % r);
        }

        public static Good operator +(Good l, Good r)
        {
            return new Good(l.Type, l.Value + r.Value);
        }

        public static Good operator -(Good l, Good r)
        {
            return new Good(l.Type, l.Value - r.Value);
        }

        public static Good operator *(Good l, Good r)
        {
            return new Good(l.Type, l.Value * r.Value);
        }

        public static Good operator /(Good l, Good r)
        {
            return new Good(l.Type, l.Value / r.Value);
        }

        public static Good operator %(Good l, Good r)
        {
            return new Good(l.Type, l.Value % r.Value);
        }

        public static Good operator ++(Good s)
        {
            return new Good(s.Type, s.Value + 1);
        }

        public static Good operator --(Good s)
        {
            return new Good(s.Type, s.Value + 1);
        }

        public static bool operator >(Good l, Good r)
        {
            return l.CompareTo(r) == 1;
        }

        public static bool operator >(Good l, int r)
        {
            return l.Value.CompareTo(r) == 1;
        }

        public static bool operator <(Good l, Good r)
        {
            return l.CompareTo(r) == -1;
        }

        public static bool operator <(Good l, int r)
        {
            return l.Value.CompareTo(r) == -1;
        }

        public static bool operator >=(Good l, Good r)
        {
            return l.CompareTo(r) >= 0;
        }

        public static bool operator >=(Good l, int r)
        {
            return l.Value.CompareTo(r) >= 0;
        }

        public static bool operator <=(Good l, Good r)
        {
            return l.CompareTo(r) <= 0;
        }

        public static bool operator <=(Good l, int r)
        {
            return l.Value.CompareTo(r) <= 0;
        }

        public static explicit operator int(Good good)
        {
            return good.Value;
        }

        public static explicit operator Category(Good good)
        {
            return good.Type;
        }

    }
}