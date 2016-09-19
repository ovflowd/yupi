using System;
using System.Numerics;

namespace Yupi.Crypto.Utils
{
    public static class BigIntegerMethods
    {
        public static int BitLength(this BigInteger scope)
        {
            if (scope <= 0)
            {
                return -1;
            }

            return (int)BigInteger.Log(scope - 1, 2);
        }

        public static int GetLowestSetBit(this BigInteger scope)
        {
            if (scope <= 0)
            {
                return -1;
            }

            return (int)BigInteger.Log(scope & -scope, 2);
        }

        public static BigInteger ModInverse(this BigInteger a, BigInteger n)
        {
            BigInteger i = n;
            BigInteger v = 0;
            BigInteger d = 1;

            while (a > 0)
            {
                BigInteger t = i / a;
                BigInteger x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;

            if (v < 0)
            {
                v = (v + n) % n;
            }

            return v;
        }

        public static byte[] ToByteArray(this BigInteger scope, bool asLittleEndian)
        {
            byte[] result = scope.ToByteArray();

            if (!asLittleEndian)
            {
                Array.Reverse(result);
            }

            return result;
        }
    }
}
