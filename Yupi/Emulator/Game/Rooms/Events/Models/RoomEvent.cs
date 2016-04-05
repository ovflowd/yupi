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

namespace Yupi.Emulator.Game.Rooms.Events.Models
{
    /// <summary>
    ///     Class RoomEvent.
    /// </summary>
     class RoomEvent
    {
        /// <summary>
        ///     The category
        /// </summary>
         int Category;

        /// <summary>
        ///     The description
        /// </summary>
         string Description;

        /// <summary>
        ///     The name
        /// </summary>
         string Name;

        /// <summary>
        ///     The room identifier
        /// </summary>
         uint RoomId;

        /// <summary>
        ///     The time
        /// </summary>
         int Time;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomEvent" /> class.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="time">The time.</param>
        /// <param name="category">The category.</param>
         RoomEvent(uint roomId, string name, string description, int time = 0, int category = 1)
        {
            RoomId = roomId;
            Name = name;
            Description = description;

            Time = time == 0 ? Yupi.GetUnixTimeStamp() + 7200 : time;

            Category = category;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance has expired.
        /// </summary>
        /// <value><c>true</c> if this instance has expired; otherwise, <c>false</c>.</value>
         bool HasExpired => Yupi.GetUnixTimeStamp() > Time;
    }
}