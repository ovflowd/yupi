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

namespace Yupi.Game.RoomBots.Interfaces
{
    /// <summary>
    ///     Class CatalogBot.
    /// </summary>
    internal class CatalogBot
    {
        /// <summary>
        ///     The bot gender
        /// </summary>
        internal string BotGender;

        /// <summary>
        ///     The bot look
        /// </summary>
        internal string BotLook;

        /// <summary>
        ///     The bot mission
        /// </summary>
        internal string BotMission;

        /// <summary>
        ///     The bot name
        /// </summary>
        internal string BotName;

        /// <summary>
        ///     The bot type
        /// </summary>
        internal string BotType;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CatalogBot" /> class.
        /// </summary>
        /// <param name="row">The row.</param>
        internal CatalogBot(DataRow row)
        {
            BotType = row["bot_type"].ToString();
            BotName = row["bot_name"].ToString();
            BotLook = row["bot_look"].ToString();
            BotMission = row["bot_mission"].ToString();
            BotGender = row["bot_gender"].ToString();
        }
    }
}