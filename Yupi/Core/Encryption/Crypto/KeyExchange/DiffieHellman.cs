/**
Because i love chocolat...
                                    88 88  
                                    "" 88  
                                       88  
8b d8 88       88 8b, dPPYba,  88 88  
`8b d8' 88       88 88P'    "8a 88 88  
 `8b d8'  88       88 88       d8 88 ""  
  `8b, d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
    Y88'     `"YbbdP'Y8 88`YbbdP"'  88 88  
    d8'                 88                 
   d8'                  88     

Private Habbo Hotel Emulating System
@author Claudio A. Santoro W.
   @author Kessiler R.
@version dev-beta
@license MIT
@copyright Sulake Corporation Oy
@observation All Rights of Habbo, Habbo Hotel, and all Habbo contents and it's names, is copyright from Sulake
   Corporation Oy. Yupi! has nothing linked with Sulake.
This Emulator is Only for DEVELOPMENT uses. If you're selling this you're violating Sulakes Copyright.
*/

using System;
using System.Numerics;
using Yupi.Core.Encryption.Utils;

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

        public BigInteger CalculateSharedKey(BigInteger m) => BigInteger.ModPow(m, _privateKey, Prime);

        private void Initialize(bool ignoreBaseKeys = false)
        {
            PublicKey = 0;

            Random rand = new Random();
            while (PublicKey == 0)
            {
                if (!ignoreBaseKeys)
                {
                    Prime = PrimeCalculator.GenPseudoPrime(Bitlength, 10, rand);
                    Generator = PrimeCalculator.GenPseudoPrime(Bitlength, 10, rand);
                }

                byte[] bytes = new byte[Bitlength/8];
                Randomizer.NextBytes(bytes);
                _privateKey = new BigInteger(bytes);

                if (_privateKey < 1)
                    continue;

                if (Generator > Prime)
                {
                    BigInteger temp = Prime;
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