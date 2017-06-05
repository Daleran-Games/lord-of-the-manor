using UnityEngine;
using System.Linq;
using DaleranGames;

namespace UnityEngine
{
    public static class MathExtensions
    {
        /// <summary>
        /// Casts a Vector2 to a Point
        /// </summary>
        /// <param name="v">Vector2 to cast</param>
        /// <returns>Vector2Int</returns>
        public static Vector2Int ToVector2Int(this Vector2 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));

        }

        /// <summary>
        /// Casts a Vector3 to a Vector2Int
        /// </summary>
        /// <param name="v">Vector 3 to cast</param>
        /// <returns>Vector2Int</returns>
        public static Vector2Int ToVector2Int(this Vector3 v)
        {
            return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));

        }

        public static float GetMaxAbsoluteDimmension(Vector3 vec)
        {
            float[] dims = { Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z) };
            return dims.Max();
        }

        public static float GetMinAbsoluteDimmension(Vector3 vec)
        {
            float[] dims = { Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z) };
            return dims.Min();
        }

        /// <summary>
        /// Solves 2nd degree polynomial functions in standard form (ax^2 + bx + c = 0) using the quadratic formula x=(-b +- sqrt(b^2 - 4ac))/2a.
        /// </summary>
        /// <param name="a">The constant in front of the x^2.</param>
        /// <param name="b">the constant in front of the x.</param>
        /// <param name="c">The constant at the end of a standard form polynomial.</param>
        /// <returns>Returns a length 2 float array with both roots of the polynomial. The first root is the addition part of the quadratic and the second root is the subtraction.</returns>
        public static float[] QuadraticSolver(float a, float b, float c)
        {
            float[] roots = new float[2];
            roots[0] = (-b + Mathf.Sqrt(b * b - 4f * a *c)) / (2f * a);
            roots[1] = (-b - Mathf.Sqrt(b * b - 4f * a * c)) / (2f * a);

            return roots;
        }

        public static bool RandomBool ()
        {
            if (Random.value >= 0.5f)
            {
                return true;
            }
            return false;
        }

        public static float ClampPositive (float number)
        {
            if (number < 0f)
                return 0f;
            else
                return number;
        }

    }
}
