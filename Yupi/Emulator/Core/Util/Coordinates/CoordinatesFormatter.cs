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

using System.Drawing;

namespace Yupi.Core.Util.Coordinates
{
    /// <summary>
    ///     Class CoordinatesFormatter.
    /// </summary>
    internal class CoordinatesFormatter
    {
        /// <summary>
        ///     Points to int.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>System.Int32.</returns>
        internal static int PointToInt(Point p) => CombineXyCoord(p.X, p.Y);

        /// <summary>
        ///     Combines the xy coord.
        /// </summary>
        /// <param name="a">a.</param>
        /// <param name="b">The b.</param>
        /// <returns>System.Int32.</returns>
        internal static int CombineXyCoord(int a, int b) => (a << 16) | b;
    }
}