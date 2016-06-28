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

        /// <summary>
        ///     Gets the message.
        /// </summary>
        /// <param name="furniId">The furni identifier.</param>
        /// <param name="pet1">The pet1.</param>
        /// <param name="pet2">The pet2.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public static SimpleServerMessageBuffer GetMessage(uint furniId, Pet pet1, Pet pet2)
        {
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("PetBreedMessageComposer"));

            messageBuffer.AppendInteger(furniId);
            messageBuffer.AppendInteger(pet1.PetId);
            messageBuffer.AppendString(pet1.Name);
            messageBuffer.AppendInteger(pet1.Level);
            messageBuffer.AppendString(pet1.Look);
            messageBuffer.AppendString(pet1.OwnerName);
            messageBuffer.AppendInteger(pet2.PetId);
            messageBuffer.AppendString(pet2.Name);
            messageBuffer.AppendInteger(pet2.Level);
            messageBuffer.AppendString(pet2.Look);
            messageBuffer.AppendString(pet2.OwnerName);
            messageBuffer.AppendInteger(4);

            messageBuffer.AppendInteger(1);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    messageBuffer.AppendInteger(TerrierEpicRace.Length);

                    foreach (int value in TerrierEpicRace)
                        messageBuffer.AppendInteger(value);

                    break;

                case "pet_bear":
                    messageBuffer.AppendInteger(BearEpicRace.Length);

                    foreach (int value in BearEpicRace)
                        messageBuffer.AppendInteger(value);

                    break;
            }

            messageBuffer.AppendInteger(2);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    messageBuffer.AppendInteger(TerrierRareRace.Length);

                    foreach (int value in TerrierRareRace)
                        messageBuffer.AppendInteger(value);

                    break;

                case "pet_bear":
                    messageBuffer.AppendInteger(BearRareRace.Length);

                    foreach (int value in BearRareRace)
                        messageBuffer.AppendInteger(value);

                    break;
            }

            messageBuffer.AppendInteger(3);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    messageBuffer.AppendInteger(TerrierNoRareRace.Length);

                    foreach (int value in TerrierNoRareRace)
                        messageBuffer.AppendInteger(value);

                    break;

                case "pet_bear":
                    messageBuffer.AppendInteger(BearNoRareRace.Length);

                    foreach (int value in BearNoRareRace)
                        messageBuffer.AppendInteger(value);

                    break;
            }

            messageBuffer.AppendInteger(94);

            switch (pet1.Type)
            {
                case "pet_terrier":
                    messageBuffer.AppendInteger(TerrierNormalRace.Length);

                    foreach (int value in TerrierNormalRace)
                        messageBuffer.AppendInteger(value);

                    break;

                case "pet_bear":
                    messageBuffer.AppendInteger(BearNormalRace.Length);

                    foreach (int value in BearNormalRace)
                        messageBuffer.AppendInteger(value);

                    break;
            }

            messageBuffer.AppendInteger(pet1.Type == "pet_terrier"
                ? PetTypeManager.GetPetRaceIdByType("pet_terrierbaby")
                : PetTypeManager.GetPetRaceIdByType("pet_bearbaby"));

            return messageBuffer;
        }
    }
}