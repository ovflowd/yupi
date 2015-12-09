#region

using System;

#endregion

namespace Yupi.Core.Encryption.Utils
{
    public class Randomizer
    {
        private static readonly Random Rand = new Random();

        public static Random GetRandom => Rand;

        public static int Next() => Rand.Next();

        public static int Next(int max) => Rand.Next(max);

        public static int Next(int min, int max) => Rand.Next(min, max);

        public static double NextDouble() => Rand.NextDouble();

        public static byte NextByte() => (byte)Next(0, 255);

        public static byte NextByte(int max)
        {
            max = Math.Min(max, 255);
            return (byte)Next(0, max);
        }

        public static byte NextByte(int min, int max)
        {
            max = Math.Min(max, 255);
            return (byte)Next(Math.Min(min, max), max);
        }

        public static void NextBytes(byte[] toparse)
        {
            Rand.NextBytes(toparse);
        }
    }
}