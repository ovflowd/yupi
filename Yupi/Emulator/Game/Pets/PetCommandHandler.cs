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

using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Pets.Structs;

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class PetCommandHandler.
    /// </summary>
    internal class PetCommandHandler
    {
        /// <summary>
        ///     The _pet commands
        /// </summary>
        private static Dictionary<uint, PetCommand> _petCommands;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal static void Init(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM pets_commands");

            _petCommands = new Dictionary<uint, PetCommand>();

            DataTable table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                _petCommands.Add(uint.Parse(dataRow["id"].ToString()), new PetCommand(dataRow));
        }

        internal static Dictionary<uint, PetCommand> GetAllPetCommands()
            => _petCommands.ToDictionary(p => p.Key, p => p.Value);

        internal static Dictionary<uint, PetCommand> GetPetCommandByPetType(string petType)
            =>
                _petCommands.Where(p => p.Value.PetTypes.Contains(petType.ToString()))
                    .ToDictionary(p => p.Key, p => p.Value);

        internal static int GetPetCommandCountByPetType(string petType)
            => _petCommands.Count(p => p.Value.PetTypes.Contains(petType.ToString()));

        internal static PetCommand GetPetCommandById(uint commandId)
            => _petCommands.FirstOrDefault(p => p.Key == commandId).Value;

        internal static PetCommand GetPetCommandByInput(string userInput)
            => _petCommands.FirstOrDefault(p => p.Value.CommandInput.Contains(userInput)).Value;
    }
}