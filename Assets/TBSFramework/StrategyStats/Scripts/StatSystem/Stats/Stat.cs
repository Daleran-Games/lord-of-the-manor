using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct Stat : IFormattable, IEquatable<Stat>, IComparable<Stat>, IComparable
    {
        public enum Category
        {
            None = 0,
            FoodYield = 1,
            WoodYield = 2,
            StoneYield = 3,
            GoldYield = 4,
            MovementCost = 5,
            DefenseBonus = 6,
            ActionPoints = 7,
            AttackPerPop = 8,
            Attack = 9,
            DefensePerPop = 10,
            Defense = 11,
            BirthRate = 12,
            DeathRate = 13,
            FoodPerPopPerTurn = 14,
            WoodPerPopPerWinter = 16,
            FoodPerAnimalPerWinter = 17,
            LivestockBirthRate = 18,
            LivestockDeathRate = 19,
            LivestockFoodYield = 20,
            LivestockGoldYield = 21,
            LivestockGoldOnSlaughter = 22,
            LivestockFoodOnSlaughter = 23,
            MaxFoodStorage = 24,
            MaxWoodStorage = 25,
            MaxStoneStorage = 26,
            MaxPopulation = 27,
            StarvationRate = 28,
            FreezingRate = 29,
            AttackCost = 30
        }


        [SerializeField]
        Category _type;
        [SerializeField]
        int _value;

        public Category Type { get { return _type; } }
        public int Value { get { return _value; } }

        public Stat (Category type, int val)
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

        public Stat ToType(Category t)
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

        public static Stat operator +(Stat l, Stat r)
        {
            return new Stat(l.Type, l.Value + r.Value);
        }

        public static Stat operator -(Stat l, Stat r)
        {
            return new Stat(l.Type, l.Value - r.Value);
        }

        public static Stat operator *(Stat l, Stat r)
        {
            return new Stat(l.Type, l.Value * r.Value);
        }

        public static Stat operator /(Stat l, Stat r)
        {
            return new Stat(l.Type, l.Value / r.Value);
        }

        public static Stat operator %(Stat l, Stat r)
        {
            return new Stat(l.Type, l.Value % r.Value);
        }

        public static Stat operator ++(Stat s)
        {
            return new Stat(s.Type, s.Value +1);
        }

        public static Stat operator --(Stat s)
        {
            return new Stat(s.Type, s.Value + 1);
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

        public static implicit operator int(Stat s)
        {
            return s.Value;
        }

        public static implicit operator Category(Stat s)
        {
            return s.Type;
        }
    }
}

