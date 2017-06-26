using System;
using UnityEngine;


namespace UnityEngine
{

    [System.Serializable]
    public struct Vector2Int : IFormattable, IEquatable<Vector2Int>, IComparable<Vector2Int>, IComparable
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;

        /// <summary>
        /// Shorthand for writing Vector2Int(0,-1).
        /// </summary>
        public static Vector2Int down = new Vector2Int(0, -1);

        public static Vector2Int left = new Vector2Int(-1, 0);

        public static Vector2Int one = new Vector2Int(1, 1);

        public static Vector2Int right = new Vector2Int(1, 0);

        public static Vector2Int up = new Vector2Int(0, 1);

        public static Vector2Int zero = new Vector2Int(0, 0);

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(Vector2Int other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.x == x && other.y == y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Vector2Int) && Equals((Vector2Int)obj);
        }

        public int CompareTo(Vector2Int other)
        {

            if (other.sqrMagnitude() > sqrMagnitude())
                return 1;
            else if (Equals(this,other))
                return 0;
            else
                return -1;

        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj);        

        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode()) ^ y.GetHashCode();
            }
        }

        public static bool operator ==(Vector2Int l, Vector2Int r)
        {
            return Equals(l, r);
        }

        public static bool operator !=(Vector2Int l, Vector2Int r)
        {
            return !Equals(l, r);
        }

        public static Vector2Int operator +(Vector2Int l, Vector2Int r)
        {
            return new Vector2Int(l.x + r.x, l.y + r.y);
        }

        public static Vector2Int operator -(Vector2Int l, Vector2Int r)
        {
            return new Vector2Int(l.x - r.x, l.y - r.y);
        }

        public static Vector2Int operator *(Vector2Int l, int r)
        {
            return new Vector2Int(l.x * r, l.y * r);
        }

        public static Vector2Int operator /(Vector2Int l, int r)
        {
            return new Vector2Int(l.x / r, l.y / r);
        }

        public static bool operator > (Vector2Int l, Vector2Int r)
        {
            return l.CompareTo(r) == 1;
        }

        public static bool operator < (Vector2Int l, Vector2Int r)
        {
            return l.CompareTo(r) == -1;
        }

        public static bool operator >=(Vector2Int l, Vector2Int r)
        {
            return l.CompareTo(r) >= 0;
        }

        public static bool operator <=(Vector2Int l, Vector2Int r)
        {
            return l.CompareTo(r) <= 0;
        }

        static public explicit operator Vector2(Vector2Int v2)
        {
            return new Vector2(v2.x, v2.y);
        }

        static public explicit operator Vector3(Vector2Int v2)
        {
            return new Vector3(v2.x, v2.y, 0f);
        }

        public float magnitude()
        {
            return Mathf.Sqrt((x * x) + (y * y));
        }

        public int sqrMagnitude()
        {
            return ((x * x) + (y * y));
        }


    }
}
