#region

using System;
using System.Numerics;
using Yupi.Core.Encryption.Utils;

#endregion

namespace Yupi.Core.Encryption.Crypto.KeyExchange
{
    public class DiffieHellman
    {
        public readonly int Bitlength = 32;

        private BigInteger _privateKey;

        public DiffieHellman()
        {
            Initialize();
        }

        public DiffieHellman(int b)
        {
            Bitlength = b;

            Initialize();
        }

        public DiffieHellman(BigInteger prime, BigInteger generator)
        {
            Prime = prime;
            Generator = generator;

            Initialize(true);
        }

        public BigInteger Prime { get; private set; }

        public BigInteger Generator { get; private set; }

        public BigInteger PublicKey { get; private set; }

        public BigInteger CalculateSharedKey(BigInteger m)
        {
            return BigInteger.ModPow(m, _privateKey, Prime);
        }

        private void Initialize(bool ignoreBaseKeys = false)
        {
            PublicKey = 0;

            var rand = new Random();
            while (PublicKey == 0)
            {
                if (!ignoreBaseKeys)
                {
                    Prime = PrimeCalculator.GenPseudoPrime(Bitlength, 10, rand);
                    Generator = PrimeCalculator.GenPseudoPrime(Bitlength, 10, rand);
                }

                var bytes = new byte[Bitlength / 8];
                Randomizer.NextBytes(bytes);
                _privateKey = new BigInteger(bytes);

                if (_privateKey < 1)
                    continue;

                if (Generator > Prime)
                {
                    var temp = Prime;
                    Prime = Generator;
                    Generator = temp;
                }

                PublicKey = BigInteger.ModPow(Generator, _privateKey, Prime);

                if (!ignoreBaseKeys)
                    break;
            }
        }
    }
}