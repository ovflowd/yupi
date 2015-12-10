/**
     Because i love chocolat...                                      
                                    88 88  
                                    "" 88  
                                       88  
8b       d8 88       88 8b,dPPYba,  88 88  
`8b     d8' 88       88 88P'    "8a 88 88  
 `8b   d8'  88       88 88       d8 88 ""  
  `8b,d8'   "8a,   ,a88 88b,   ,a8" 88 aa  
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

using System.Globalization;
using System.Numerics;
using System.Text;
using Yupi.Core.Encryption.Crypto.KeyExchange;
using Yupi.Core.Encryption.Hurlant.Crypto.Rsa;
using Yupi.Core.Encryption.Utils;
using Yupi.Core.Io;

namespace Yupi.Core.Encryption
{
    public class Handler
    {
        public static RsaKey Rsa;
        public static DiffieHellman DiffieHellman;

        public static void Initialize(string n, string d, string e)
        {
            Rsa = RsaKey.ParsePrivateKey('0' + n, '0' + e, '0' + d);

            DiffieHellman = new DiffieHellman();
        }

        public static string GetRsaDiffieHellmanPrimeKey() => GetRsaStringEncrypted(DiffieHellman.Prime.ToString());

        public static string GetRsaDiffieHellmanGeneratorKey() => GetRsaStringEncrypted(DiffieHellman.Generator.ToString());

        public static string GetRsaDiffieHellmanPublicKey() => GetRsaStringEncrypted(DiffieHellman.PublicKey.ToString());

        public static BigInteger CalculateDiffieHellmanSharedKey(string publicKey)
        {
            try
            {
                var bytes = BigInteger.Parse('0' + publicKey, NumberStyles.HexNumber).ToByteArray();
                var keyBytes = Rsa.Verify(bytes);
                var keyString = Encoding.Default.GetString(keyBytes);

                return DiffieHellman.CalculateSharedKey(BigInteger.Parse(keyString));
            }
            catch
            {
                Writer.LogCriticalException("Sorry, the Encryption Handler stopped Inesperatelly. Please Restart Emulator.");
                return 0;
            }
        }

        private static string GetRsaStringEncrypted(string message)
        {
            try
            {
                var m = Encoding.Default.GetBytes(message);
                var c = Rsa.Sign(m);

                return Converter.BytesToHexString(c);
            }
            catch
            {
                Writer.LogCriticalException("Sorry, the Encryption Handler stopped Inesperatelly. Please Restart Emulator.");
                return null;
            }
        }
    }
}