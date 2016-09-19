using System.Security.Cryptography;

namespace Yupi.Crypto.Utils
{
    public static class Randomizer
    {
        private static RandomNumberGenerator _random;

        public static RandomNumberGenerator GetRandom()
        {
            if (_random == null)
            {
                _random = new RNGCryptoServiceProvider();
            }

            return _random;
        }
    }
}
