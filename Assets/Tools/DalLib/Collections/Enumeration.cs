using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DaleranGames
{
    [System.Serializable]
    public abstract class Enumeration : IFormattable, IEquatable<Enumeration>, IComparable<Enumeration>, IComparable
    {
        [SerializeField]
        int _value;
        [SerializeField]
        string _name;

        protected Enumeration()
        {
        }

        protected Enumeration(int value, string name)
        {
            _value = value;
            _name = name;
        }

        public int Value
        {
            get { return _value; }
        }

        public string Name
        {
            get { return _name; }
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;

                if (locatedValue != null)
                {
                    yield return locatedValue;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = _value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration, new()
        {
            var matchingItem = parse<T, int>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        public static T FromName<T>(string name) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(name, "display name", item => item.Name == name);
            return matchingItem;
        }

        private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public bool Equals(Enumeration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GetType().Equals(other.GetType()) && _value.Equals(other.Value);
        }

        public int CompareTo(Enumeration other)
        {
            return Value.CompareTo(other.Value);
        }

        public int CompareTo(object other)
        {
            return Value.CompareTo(((Enumeration)other).Value);
        }

        public static bool operator ==(Enumeration l, Enumeration r)
        {
            return Equals(l, r);
        }

        public static bool operator !=(Enumeration l, Enumeration r)
        {
            return !Equals(l, r);
        }

        public static bool operator >(Enumeration l, Enumeration r)
        {
            return l.CompareTo(r) == 1;
        }

        public static bool operator <(Enumeration l, Enumeration r)
        {
            return l.CompareTo(r) == -1;
        }

        public static bool operator >=(Enumeration l, Enumeration r)
        {
            return l.CompareTo(r) >= 0;
        }

        public static bool operator <=(Enumeration l, Enumeration r)
        {
            return l.CompareTo(r) <= 0;
        }

        public static explicit operator int(Enumeration e)
        {
            return e.Value;
        }
    }

}