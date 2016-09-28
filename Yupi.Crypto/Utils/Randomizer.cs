namespace Yupi.Crypto.Utils
{
    using System.Security.Cryptography;

    public static class Randomizer
    {
        #region Fields

        private static RandomNumberGenerator _random;

        #endregion Fields

        #region Methods

        public static RandomNumberGenerator GetRandom()
        {
            if (_random == null)
            {
                _random = new RNGCryptoServiceProvider();
            }

            return _random;
        }

        #endregion Methods
    }
}