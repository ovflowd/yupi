using System.Security.Cryptography;
using System.Text;

namespace Yupi.Util
{
    public class Cryptography
    {
        public static string GetUniqueKey(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var data = new byte[length];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
            }

            var result = new StringBuilder(length);
            foreach (var b in data) result.Append(chars[b%chars.Length]);
            return result.ToString();
        }
    }
}