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

using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class PetBreeding.
    /// </summary>
    internal class PetBreeding
    {
        /// <summary>
        ///     The terrier epic race
        /// </summary>
        internal static int[] TerrierEpicRace = {17, 18, 19};

        /// <summary>
        ///     The terrier rare race
        /// </summary>
        internal static int[] TerrierRareRace = {10, 14, 15, 16};

        /// <summary>
        ///     The terrier no rare race
        /// </summary>
        internal static int[] TerrierNoRareRace = {11, 12, 13, 4, 5, 6};

        /// <summary>
        ///     The terrier normal race
        /// </summary>
        internal static int[] TerrierNormalRace = {0, 1, 2, 7, 8, 9};

        /// <summary>
        ///     The bear epic race
        /// </summary>
        internal static int[] BearEpicRace = {3, 10, 11};

        /// <summary>
        ///     The bear rare race
        /// </summary>
        internal static int[] BearRareRace = {12, 13, 15, 16, 17, 18};

        /// <summary>
        ///     The bear no rare race
        /// </summary>
        internal static int[] BearNoRareRace = {7, 8, 9, 14, 19};

        /// <summary>
        ///     The bear normal race
        /// </summary>
        internal static int[] BearNormalRace = {0, 1, 2, 4, 5, 6};

        /// <summary>
        ///     Gets the message.
        /// </summary>
        /// <param name="furniId">The furni identifier.</param>
        /// <param name="pet1">The pet1.</param>
        /// <param name="pet2">The pet2.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage GetMessage(uint furniId, Pet pet1, Pet pet2)
        {
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("PetBreedMessageComposer"));

            message.AppendInteger(furniId);
            message.AppendInteger(pet1.PetId);
            message.AppendString(pet1.Name);
            message.AppendInteger(pet1.Level);
            message.AppendString(pet1.Look);
            message.AppendString(pet1.OwnerName);
            message.AppendInteger(pet2.PetId);
            message.AppendString(pet2.Name);
            message.AppendInteger(pet2.Level);
            message.AppendString(pet2.Look);
            message.AppendString(pet2.OwnerName);
            message.AppendInteger(4);

            message.AppendInteger(1);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    message.AppendInteger(TerrierEpicRace.Length);

                    foreach (int value in TerrierEpicRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(BearEpicRace.Length);

                    foreach (int value in BearEpicRace)
                        message.AppendInteger(value);

                    break;
            }

            message.AppendInteger(2);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    message.AppendInteger(TerrierRareRace.Length);

                    foreach (int value in TerrierRareRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(BearRareRace.Length);

                    foreach (int value in BearRareRace)
                        message.AppendInteger(value);

                    break;
            }

            message.AppendInteger(3);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    message.AppendInteger(TerrierNoRareRace.Length);

                    foreach (int value in TerrierNoRareRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(BearNoRareRace.Length);

                    foreach (int value in BearNoRareRace)
                        message.AppendInteger(value);

                    break;
            }

            message.AppendInteger(94);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    message.AppendInteger(TerrierNormalRace.Length);

                    foreach (int value in TerrierNormalRace)
                        message.AppendInteger(value);

                    break;

                case "pet_bear":
                    message.AppendInteger(BearNormalRace.Length);

                    foreach (int value in BearNormalRace)
                        message.AppendInteger(value);

                    break;
            }

            message.AppendInteger(pet1.Type == "pet_terrier"
                ? PetTypeManager.GetPetRaceIdByType("pet_terrierbaby")
                : PetTypeManager.GetPetRaceIdByType("pet_bearbaby"));

            return message;
        }
    }
}