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

using System;
using System.IO;
using System.IO.Compression;

namespace Yupi.Core.Encryption.Utils
{
    public class Converter
    {
        public static string BytesToHexString(byte[] bytes)
        {
            string hexstring = BitConverter.ToString(bytes);

            return hexstring.Replace("-", string.Empty);
        }

        public static byte[] HexStringToBytes(string hexstring)
        {
            int numberChars = hexstring.Length;

            byte[] bytes = new byte[numberChars/2];

            for (int i = 0; i < numberChars; i += 2)
                bytes[i/2] = Convert.ToByte(hexstring.Substring(i, 2), 16);

            return bytes;
        }

        public static string Deflate(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes, 2, bytes.Length - 2))

            using (DeflateStream inflater = new DeflateStream(stream, CompressionMode.Decompress))

            using (StreamReader streamReader = new StreamReader(inflater))
                return streamReader.ReadToEnd();
        }
    }
}