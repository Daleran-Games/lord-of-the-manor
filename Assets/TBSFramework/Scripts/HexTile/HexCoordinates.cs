using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.TBSFramework
{
    [System.Serializable]
    public struct HexCoordinates
    {
        [SerializeField]
        [ReadOnly]
        int r;
        public int Q { get { return r; } }

        [SerializeField]
        [ReadOnly]
        int q;
        public int R { get { return q; } }

        public int S { get { return  - Q - R; } }

        public HexCoordinates(int x, int y)
        {
            this.r = x;
            this.q = y;
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
            return q + r / 2;
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
       
        public override string ToString()
        {
            return "(" + Q.ToString() + "," + R.ToString() + "," + S.ToString() + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return Q.ToString() + "\n" + R.ToString() + "\n" + S.ToString();
        }
    }

}

