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
            Vector3 position;
            position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);
            position.y = y * (HexMetrics.outerRadius * 1.5f);
            position.z = 0f;

            return position;
        }

        public static Vector3 GetUnityPosition(int x, int y, float z)
        {
            Vector3 position;
            position.x = (x + y * 0.5f - y / 2) * (HexMetrics.innerRadius * 2f);
            position.y = y * (HexMetrics.outerRadius * 1.5f);
            position.z = z;

            return position;
        }

        public static Vector2Int GetCartesianFromUnity (Vector3 position)
        {
            float y = position.y / (HexMetrics.outerRadius * 1.5f);

            return new Vector2Int ( Mathf.RoundToInt((position.x / (HexMetrics.innerRadius * 2f)) - y * 0.5f + y / 2),
                Mathf.RoundToInt(y));
        }
       
        public override string ToString()
        {
            return "(" + Q.ToString() + ", " + R.ToString() + ", " + S.ToString() + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return Q.ToString() + "\n" + R.ToString() + "\n" + S.ToString();
        }
    }

}

