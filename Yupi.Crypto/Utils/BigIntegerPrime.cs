namespace Yupi.Crypto.Utils
{
    using System.Numerics;
    using System.Security.Cryptography;

    public static class BigIntegerPrime
    {
        #region Methods

        public static BigInteger GeneratePseudoPrime(int bitLength, int certainty, RandomNumberGenerator random)
        {
            byte[] bytes = new byte[(bitLength + 7) / 8];

            BigInteger result = 0;
            do
            {
                random.GetBytes(bytes);
                bytes[bytes.Length - 1] = 0;
                result = new BigInteger(bytes);
            }
            while (result.IsProbablePrime(certainty, random));

            return result;
        }

        public static bool IsProbablePrime(this BigInteger source, int certainty, RandomNumberGenerator random)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            if (random == null)
            {
                random = Randomizer.GetRandom();
            }

            byte[] bytes = new byte[(source.BitLength() + 7) / 8];
            BigInteger a;

            for (int i = 0; i < certainty; i++)
            {
                do
                {
                    random.GetBytes(bytes);
                    //bytes[bytes.Length - 1] = 0;
                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= source - 2);

                BigInteger x = BigInteger.ModPow(a, d, source);
                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }

        #endregion Methods
    }
}