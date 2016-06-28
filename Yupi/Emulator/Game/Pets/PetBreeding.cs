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

using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Pets
{
    /// <summary>
    ///     Class PetBreeding.
    /// </summary>
     public class PetBreeding
    {
        /// <summary>
        ///     The terrier epic race
        /// </summary>
     public static int[] TerrierEpicRace = {17, 18, 19};

        /// <summary>
        ///     The terrier rare race
        /// </summary>
     public static int[] TerrierRareRace = {10, 14, 15, 16};

        /// <summary>
        ///     The terrier no rare race
        /// </summary>
     public static int[] TerrierNoRareRace = {11, 12, 13, 4, 5, 6};

        /// <summary>
        ///     The terrier normal race
        /// </summary>
     public static int[] TerrierNormalRace = {0, 1, 2, 7, 8, 9};

        /// <summary>
        ///     The bear epic race
        /// </summary>
     public static int[] BearEpicRace = {3, 10, 11};

        /// <summary>
        ///     The bear rare race
        /// </summary>
     public static int[] BearRareRace = {12, 13, 15, 16, 17, 18};

        /// <summary>
        ///     The bear no rare race
        /// </summary>
     public static int[] BearNoRareRace = {7, 8, 9, 14, 19};

        /// <summary>
        ///     The bear normal race
        /// </summary>
     public static int[] BearNormalRace = {0, 1, 2, 4, 5, 6};
    }
}