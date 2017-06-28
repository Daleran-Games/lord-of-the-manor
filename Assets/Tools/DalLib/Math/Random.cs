using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    public static class Random
    {

        private static readonly System.Random seedGenerator = new System.Random();
        public static int seed = seedGenerator.Next();
        //public static int seed = 5;
        public static int Seed
        {
            get { return seed; }
            set
            {
                seed = value;
                random = new System.Random(seed);
            }
        }

        private static System.Random random = new System.Random();
        private static readonly object syncLock = new object();

        public static bool Bool()
        {
            lock(syncLock)
            {
                if (random.NextDouble() >= 0.5)
                {
                    return true;
                }
                return false;
            }
        }

        public static int Int()
        {
            lock (syncLock)
            {
                return random.Next();
            }
        }

        public static int Int(int max)
        {
            lock (syncLock)
            {
                return random.Next(max);
            }
        }

        public static int Int(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }

        public static float Float()
        {
            lock (syncLock)
            {
                double mantissa = (random.NextDouble() * 2.0) - 1.0;
                double exponent = System.Math.Pow(2.0, random.Next(-126, 128));
                return (float)(mantissa * exponent);
            }
        }

        public static float Float(float max)
        {
            lock (syncLock)
            {
                return (float)random.NextDouble() * max;
            }
        }

        public static float Float(float min, float max)
        {
            lock (syncLock)
            {
                return (float)random.NextDouble() * (max - min) + min;
            }
        }

        public static float Float01()
        {
            lock (syncLock)
            {
                return (float)random.NextDouble();
            }
        }
    }
}