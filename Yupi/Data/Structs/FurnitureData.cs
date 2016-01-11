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

namespace Yupi.Data.Structs
{
    /// <summary>
    ///     Struct FurnitureData
    /// </summary>
    public struct FurnitureData
    {
        /// <summary>
        ///     The identifier
        /// </summary>
        public int Id;

        /// <summary>
        ///     The name
        /// </summary>
        public string Name;

        /// <summary>
        ///     The x
        /// </summary>
        public ushort X, Y;

        /// <summary>
        ///     The can sit
        /// </summary>
        public bool CanSit, CanWalk;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FurnitureData" /> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="canSit">if set to <c>true</c> [can sit].</param>
        /// <param name="canWalk">if set to <c>true</c> [can walk].</param>
        public FurnitureData(int id, string name, ushort x = 0, ushort y = 0, bool canSit = false, bool canWalk = false)
        {
            Id = id;
            Name = name;
            X = x;
            Y = y;
            CanSit = canSit;
            CanWalk = canWalk;
        }
    }
}