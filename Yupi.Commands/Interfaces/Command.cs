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

using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Interfaces
{
    /// <summary>
    ///     Class Command.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        ///     Gets or sets the minimum rank.
        /// </summary>
        /// <value>The minimum rank.</value>
		virtual short MinRank { public get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
		virtual string Description { public get; set; }

        /// <summary>
        ///     Gets or sets the usage.
        /// </summary>
        /// <value>The usage.</value>
		virtual string Usage { public get; set; }

        /// <summary>
        ///     Gets or sets the alias.
        /// </summary>
        /// <value>The alias.</value>
		virtual string Alias { public get; set; }

        /// <summary>
        ///     Gets or sets the minimum parameters.
        /// </summary>
        /// <value>The minimum parameters.</value>
		virtual short MinParams { public get; set; }

        /// <summary>
        ///     Executes the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="pms">The PMS.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool Execute(GameClient client, string[] pms);
    }
}