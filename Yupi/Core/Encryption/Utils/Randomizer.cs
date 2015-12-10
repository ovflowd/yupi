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

namespace Yupi.Core.Encryption.Utils
{
    public class Randomizer
    {
        private static readonly Random Rand = new Random();

        public static Random GetRandom => Rand;

        public static int Next() => Rand.Next();

        public static int Next(int max) => Rand.Next(max);

        public static int Next(int min, int max) => Rand.Next(min, max);

        public static double NextDouble() => Rand.NextDouble();

        public static byte NextByte() => (byte)Next(0, 255);

        public static byte NextByte(int max)
        {
            max = Math.Min(max, 255);

            return (byte)Next(0, max);
        }

        public static byte NextByte(int min, int max)
        {
            max = Math.Min(max, 255);

            return (byte)Next(Math.Min(min, max), max);
        }

        public static void NextBytes(byte[] toparse)
        {
            Rand.NextBytes(toparse);
        }
    }
}