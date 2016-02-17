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

using System.Data;

namespace Yupi.Game.Pets.Structs
{
    /// <summary>
    ///     Class PetRace.
    /// </summary>
    internal class PetRace
    {
        /// <summary>
        ///     The color1
        /// </summary>
        public uint ColorOne;

        /// <summary>
        ///     The color2
        /// </summary>
        public uint ColorTwo;

        /// <summary>
        ///     The has1 color
        /// </summary>
        public bool HasColorOne;

        /// <summary>
        ///     The has2 color
        /// </summary>
        public bool HasColorTwo;

        /// <summary>
        ///     The race identifier
        /// </summary>
        public uint RaceId;

        public PetRace(DataRow row)
        {
            RaceId = (uint) row["race_type"];
            ColorOne = (uint) row["color_one"];
            ColorTwo = (uint) row["color_two"];
            HasColorOne = (string) row["color_one_enabled"] == "1";
            HasColorTwo = (string) row["color_two_enabled"] == "1";
        }
    }
}