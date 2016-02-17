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

namespace Yupi.Core.Util.Math
{
    /// <summary>
    ///     Class RandomNumberGenerator.
    /// </summary>
    internal static class RandomNumberGenerator
    {
        /// <summary>
        ///     The global random
        /// </summary>
        private static readonly Random GlobalRandom = new Random();

        /// <summary>
        ///     The _local random
        /// </summary>
        [ThreadStatic] private static Random _localRandom;

        /// <summary>
        ///     Gets the specified minimum.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>System.Int32.</returns>
        public static int Get(int min, int max)
        {
            Random random = _localRandom;

            if (random != null)
                return random.Next(min, max);

            int seed;

            lock (GlobalRandom)
                seed = GlobalRandom.Next();

            random = _localRandom = new Random(seed);

            return random.Next(min, max);
        }
    }
}