#region

using System;
using System.Numerics;

#endregion

namespace Yupi.Core.Encryption
{
    internal static class PrimeCalculator
    {
        public static BigInteger GenPseudoPrime(int bits, int confidence, Random rand)
        {
            BigInteger integer = 0;
            var done = false;

            while (!done)
            {
                var result = GetNextInt64(1000000000, 5000000000, rand);

                done = result % confidence != 0 && confidence % result != 0;
                if (done)
                    integer = result;
            }
            return integer;
        }

        private static long GetNextInt64(long min, long max, Random rand)
        {
            var buf = new byte[8];
            rand.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }
    }
}