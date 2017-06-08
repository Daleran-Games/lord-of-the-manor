using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public static class CollectionExtensions 
    {
        public static int RandomIndex (this System.Array array)
        {
            return Random.Range(0, array.Length-1);
        }

        public static int RandomIndex(this System.Array array, int dimension)
        {
            return Random.Range(0, array.GetLength(dimension)-1);
        }

        public static int RandomIndex<T> (this List<T> list)
        {
            return Random.Range(0, list.Count);
        }

    }
}
