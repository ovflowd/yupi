namespace Yupi.Crypto.Utils
{
    using System;
    using System.Numerics;

    public static class RSACUtils
    {
        #region Methods

        public static BigInteger Base64ToBigInteger(string data, bool asLittleEndian)
        {
            if (data == null || data == "")
            {
                return 0;
            }

            byte[] bytes = Convert.FromBase64String(data);

            if (asLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new BigInteger(bytes);
        }

        public static BigInteger Base64ToBigInteger(string data)
        {
            return Base64ToBigInteger(data, true);
        }

        public static string BigIntegerToBase64(BigInteger data, bool asLittleEndian)
        {
            if (data == null || data == 0)
            {
                return null;
            }

            byte[] bytes = data.ToByteArray();

            if (!asLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return Convert.ToBase64String(bytes);
        }

        public static string BigIntegerToBase64(BigInteger data)
        {
            return BigIntegerToBase64(data, true);
        }

        #endregion Methods
    }
}