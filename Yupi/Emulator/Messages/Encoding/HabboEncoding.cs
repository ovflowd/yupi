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

namespace Yupi.Emulator.Messages.EncodingDELETE
{
    /// <summary>
    ///     Class HabboEncoding.
    /// </summary>
    internal class HabboEncoding
    {
		// TODO Size of int and short might change depending upon the platform
        private const short ShortLength = sizeof (short);
        private const int IntLength = sizeof (int);

        /// <summary>
        ///     Decodes the int32.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="position"></param>
        /// <returns>System.Int32.</returns>
        internal static int DecodeInt32(byte[] v, ref int position)
        {
            if (position + IntLength > v.Length || position < 0)
                return 0;

            return (v[position++] << 24) + (v[position++] << 16) + (v[position++] << 8) + v[position++];
        }

        /// <summary>
        ///     Decodes the int16.
        /// </summary>
        /// <param name="v">The v.</param>
        /// <param name="position"></param>
        /// <returns>Int16.</returns>
        internal static short DecodeInt16(byte[] v, ref int position)
        {
            if (position + ShortLength > v.Length || position < 0)
                return 0;

            return (short) ((v[position++] << 8) + v[position++]);
        }

        /// <summary>
        ///     Gets the character filter.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>System.String.</returns>
        public static string GetCharFilter(string data)
        {
			// TODO Why are lower ASCII letters replaced?
            for (int i = 0; i <= 13; i++)
                data = data.Replace(Convert.ToChar(i) + "", "[" + i + "]");

            return data;
        }
    }
}