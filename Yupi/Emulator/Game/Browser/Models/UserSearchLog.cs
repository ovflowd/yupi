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

namespace Yupi.Game.Browser.Models
{
    /// <summary>
    ///     Struct NaviLogs
    /// </summary>
    internal struct UserSearchLog
    {
        /// <summary>
        ///     The identifier
        /// </summary>
        internal int Id;

        /// <summary>
        ///     The value1
        /// </summary>
        internal string Value1;

        /// <summary>
        ///     The value2
        /// </summary>
        internal string Value2;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSearchLog" /> struct.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        internal UserSearchLog(int id, string value1, string value2)
        {
            Id = id;
            Value1 = value1;
            Value2 = value2;
        }
    }
}