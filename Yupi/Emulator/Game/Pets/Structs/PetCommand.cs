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
    ///     Struct PetCommand
    /// </summary>
    internal struct PetCommand
    {
        /// <summary>
        ///     The command identifier
        /// </summary>
        internal int CommandId;

        /// <summary>
        ///     The command input
        /// </summary>
        internal string[] CommandInput;

        internal string CommandAction;

        /// <summary>
        ///     The minimum rank
        /// </summary>
        internal int MinLevel;

        internal string PetStatus;

        internal string PetGesture;

        internal uint GainedExperience;

        internal uint LostEnergy;

        internal string[] PetTypes;

        internal string PetSpeech;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PetCommand" /> struct.
        /// </summary>
        public PetCommand(DataRow row)
        {
            CommandAction = row["action"].ToString();
            CommandId = int.Parse(row["id"].ToString());
            CommandInput = row["input"].ToString().Split(';');
            MinLevel = int.Parse(row["level"].ToString());
            PetStatus = row["status"].ToString();
            PetGesture = row["gesture"].ToString();
            GainedExperience = uint.Parse(row["experience"].ToString());
            LostEnergy = uint.Parse(row["energy"].ToString());
            PetTypes = row["pet_type"].ToString().Split(',');
            PetSpeech = row["pet_speech"].ToString();
        }
    }
}