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

namespace Yupi.Game.Pathfinding
{
    /// <summary>
    ///     Class Rotation.
    /// </summary>
    internal static class Rotation
    {
        /// <summary>
        ///     Calculates the specified x1.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <returns>System.Int32.</returns>
        internal static int Calculate(int x1, int y1, int x2, int y2)
        {
            int result = 0;

            if (x1 > x2 && y1 > y2)
                result = 7;
            else if (x1 < x2 && y1 < y2)
                result = 3;
            else if (x1 > x2 && y1 < y2)
                result = 5;
            else if (x1 < x2 && y1 > y2)
                result = 1;
            else if (x1 > x2)
                result = 6;
            else if (x1 < x2)
                result = 2;
            else if (y1 < y2)
                result = 4;
            else if (y1 > y2)
                result = 0;

            return result;
        }

        /// <summary>
        ///     Calculates the specified x1.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="moonwalk">if set to <c>true</c> [moonwalk].</param>
        /// <returns>System.Int32.</returns>
        internal static int Calculate(int x1, int y1, int x2, int y2, bool moonwalk)
            => !moonwalk ? Calculate(x1, y1, x2, y2) : RotationIverse(Calculate(x1, y1, x2, y2));

        /// <summary>
        ///     Rotations the iverse.
        /// </summary>
        /// <param name="rot">The rot.</param>
        /// <returns>System.Int32.</returns>
        internal static int RotationIverse(int rot) => rot > 3 ? rot - 4 : rot + 4;
    }
}