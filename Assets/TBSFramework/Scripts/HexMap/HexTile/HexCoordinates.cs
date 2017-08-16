using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct HexCoordinates : IFormattable, IEquatable<HexCoordinates>
    {

        [SerializeField]
        int q;
        public int Q { get { return q; } }

        [SerializeField]
        int r;
        public int R { get { return r; } }

        public int S { get { return  - Q - R; } }

        public enum Dimensions
        {
            Q=0,
            R=1,
            S = 2
        }

        public enum Directions
        {
            E = 0,
            SE = 1,
            SW = 2,
            W = 3,
            NW = 4,
            NE = 5
        }

        public static HexCoordinates Zero { get { return new HexCoordinates(0, 0); } }

        public static HexCoordinates East { get { return new HexCoordinates(1, 0); } }
        public static HexCoordinates West { get { return new HexCoordinates(-1, 0); } }
        public static HexCoordinates Northeast { get { return new HexCoordinates(0, 1); } }
        public static HexCoordinates Northwest { get { return new HexCoordinates(-1, 1); } }
        public static HexCoordinates Southeast { get { return new HexCoordinates(1, -1); } }
        public static HexCoordinates Southwest { get { return new HexCoordinates(0, -1); } }

        public HexCoordinates(int q, int r)
        {
            this.q = q;
            this.r = r;
        }

        public static HexCoordinates CartesianToHex(int x, int y)
        {
            return new HexCoordinates(ToHexQ(x,y), y);
        }

        public static Vector2Int HexToCartesian (int q, int r, int s)
        {
            return new Vector2Int(ToCartesianX(q,r),r);
        }

        public static int ToHexQ(int x, int y)
        {
            return x - y / 2;
        }

        public static int ToCartesianX(int q, int r)
        {
            return q + (r>>1);
        }

        public static Vector3 GetUnityPosition(int q, int r, int s)
        {
            Vector2Int cart = HexToCartesian(q, r, s);
            return GetUnityPosition(q, r);
        }

        public static Vector3 GetUnityPosition(int x, int y)
        {
            return GetUnityPosition(x, y, 0f);
        }

        public static Vector3 GetUnityPosition(int x, int y, float z)
        { 
            Vector3 position;
            position.x = (x + y * 0.5f - y / 2) * (HexMetrics.gridWidth);
            position.y = y * (HexMetrics.gridHeight);
            position.z = z;

            return position;
        }

        public static Vector2Int GetCartesianFromUnity (Vector3 position)
        {
            float y = position.y / (HexMetrics.gridHeight) - ((HexMetrics.gridHeight) /6f);
            int _y = Mathf.RoundToInt(y);
            float x = (position.x / (HexMetrics.gridWidth)) - y * 0.5f + y / 2;
            int _x = Mathf.RoundToInt(x);

            bool rowIsOdd = _y % 2 == 1;
            Vector3 relPoint = position - GetUnityPosition(_x, _y, position.z);


            if (relPoint.y > (HexMetrics.hexSlope * relPoint.x) + HexMetrics.outerRadius)
            {
                _y++;
                if (!rowIsOdd)
                    _x--;
            } else if (relPoint.y > (-HexMetrics.hexSlope * relPoint.x) + HexMetrics.outerRadius)
            {
                _y++;
                if (rowIsOdd)
                    _x++;
            }
            return new Vector2Int(_x, _y);

        }

        public static List<HexCoordinates> GetCoordinatesInRange (HexCoordinates center, int range)
        {
            List<HexCoordinates> coords = new List<HexCoordinates>();
            //Debug.Log(center);

            /*
            for (int n = -range; n <= range; n++)
            {

                Debug.Log("M Left: "+  Math.Max(-range, -n - range) + " M Right" + Math.Min(range, -n + range));
                for (int m = Math.Max(-range, -n - range); m <= Math.Min(range, -m + range); m++)
                {
                    int newQ = center.Q + n;
                    int newR = center.R + (-n - m);

                    Debug.Log("(" + newQ  + ","+ newR + ")");
                    coords.Add(new HexCoordinates(center.Q+n, center.R+(-n-m)));
                }

            }
            */

            for (int x = -range; x <= range; x++)
            {
                for (int y = -range; y <= range; y++)
                {
                    for (int z = -range; z <= range; z++)
                    {
                        if (x+y+z == 0)
                        {
                            coords.Add(new HexCoordinates(center.Q + x, center.R + z));
                        }

                    }
                }
            }

            return coords;
        }

        public static int GetDistance (HexCoordinates start, HexCoordinates end)
        {
            return (Math.Abs(start.Q - end.Q)+ Math.Abs(start.R - end.R) + Math.Abs(start.S - end.S)) /2;
        }

        public override string ToString()
        {
            return "(" + Q.ToString() + "," + R.ToString() + "," + S.ToString() + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return Q.ToString() + "\n" + R.ToString() + "\n" + S.ToString();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();
        }

        public bool Equals(HexCoordinates other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Q == Q && other.R == R && other.S == S;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(HexCoordinates) && Equals((HexCoordinates)obj);
        }

        public override int GetHashCode()
        {
            return q & (int)0xFFFF | r << 16;
        }

        public static bool operator ==(HexCoordinates l, HexCoordinates r)
        {
            return Equals(l, r);
        }

        public static bool operator !=(HexCoordinates l, HexCoordinates r)
        {
            return !Equals(l, r);
        }

        public static HexCoordinates operator +(HexCoordinates l, HexCoordinates r)
        {
            return new HexCoordinates(l.Q + r.Q, l.R + r.R);
        }

        public static HexCoordinates operator -(HexCoordinates l, HexCoordinates r)
        {
            return new HexCoordinates(l.Q - r.Q, l.R - r.R);
        }

        public static HexCoordinates operator -(HexCoordinates l)
        {
            return new HexCoordinates(-l.Q, -l.R);
        }

        public static HexCoordinates operator *(HexCoordinates l, int r)
        {
            return new HexCoordinates(l.Q * r, l.R * r);
        }

        public static HexCoordinates operator /(HexCoordinates l, int r)
        {
            return new HexCoordinates(l.Q / r, l.R / r);
        }

    }

}

