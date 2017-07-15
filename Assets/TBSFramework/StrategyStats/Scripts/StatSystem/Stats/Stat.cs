using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Stat : IFormattable, IEquatable<Stat>, IComparable<Stat>, IComparable
    {

        [SerializeField]
        StatType _type;
        [SerializeField]
        int _value;

        public StatType Type { get { return _type; } }
        public int Value { get { return _value; } }

        public Stat (StatType type, int val)
        {
            _type = type;
            _value = val;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Type, Value);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Stat other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Type == Type && other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Stat) && Equals((Stat)obj);
        }

        public int CompareTo(Stat other)
        {
            if (other.Type == Type)
            {
                return Value.CompareTo(other.Value);
            } else
            {
                if (other.Type.Value > Type.Value)
                    return 1;
                else
                    return -1;
            }
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (ReferenceEquals(this, obj)) return 0;

            if (obj.GetType() == typeof(Stat) && Equals((Stat)obj))
                return CompareTo((Stat)obj);
            else
                throw new ArgumentException("Object is not a Stat");
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode()) ^ Type.GetHashCode();
            }
        }

        public Stat ToType(StatType t)
        {
            return new Stat(t, Value);
        }

        public static bool operator == (Stat l, Stat r)
        {
            return Equals(l, r);
        }

        public static bool operator ==(Stat l, int r)
        {
            return l.Value == r;
        }

        public static bool operator != (Stat l, Stat r)
        {
            return !Equals(l, r);
        }

        public static bool operator !=(Stat l, int r)
        {
            return !(l.Value == r);
        }

        public static bool operator >(Stat l, Stat r)
        {
            return l.CompareTo(r) == 1;
        }

        public static bool operator >(Stat l, int r)
        {
            return l.Value.CompareTo(r) == 1;
        }

        public static bool operator <(Stat l, Stat r)
        {
            return l.CompareTo(r) == -1;
        }

        public static bool operator <(Stat l, int r)
        {
            return l.Value.CompareTo(r) == -1;
        }

        public static bool operator >=(Stat l, Stat r)
        {
            return l.CompareTo(r) >= 0;
        }

        public static bool operator >=(Stat l, int r)
        {
            return l.Value.CompareTo(r) >= 0;
        }

        public static bool operator <=(Stat l, Stat r)
        {
            return l.CompareTo(r) <= 0;
        }

        public static bool operator <=(Stat l, int r)
        {
            return l.Value.CompareTo(r) <= 0;
        }

        public static Stat operator +(Stat l, int r)
        {
            return new Stat(l.Type, l.Value + r);
        }

        public static Stat operator -(Stat l, int r)
        {
            return new Stat(l.Type, l.Value - r);
        }

        public static Stat operator *(Stat l, int r)
        {
            return new Stat(l.Type, l.Value * r);
        }

        public static Stat operator /(Stat l, int r)
        {
            return new Stat(l.Type, l.Value / r);
        }

        public static Stat operator %(Stat l, int r)
        {
            return new Stat(l.Type, l.Value % r);
        }

        public static Stat operator ++(Stat s)
        {
            return new Stat(s.Type, s.Value +1);
        }

        public static Stat operator --(Stat s)
        {
            return new Stat(s.Type, s.Value - 1);
        }

        public static implicit operator int(Stat s)
        {
            return s.Value;
        }

        public static implicit operator StatType(Stat s)
        {
            return s.Type;
        }
    }
}

