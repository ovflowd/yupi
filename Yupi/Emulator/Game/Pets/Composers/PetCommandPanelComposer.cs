using System.Collections.Generic;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pets.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Pets.Composers
{
     class PetCommandPanelComposer
    {
         static void GenerateMessage(Pet pet, Dictionary<uint, PetCommand> totalPetCommands,
            Dictionary<uint, PetCommand> petCommands, GameClient owner)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("PetTrainerPanelMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(pet.PetId);

            simpleServerMessageBuffer.AppendInteger(totalPetCommands.Count);

            foreach (uint sh in totalPetCommands.Keys)
                simpleServerMessageBuffer.AppendInteger(sh);

            simpleServerMessageBuffer.AppendInteger(petCommands.Count);

            foreach (uint sh in petCommands.Keys)
                simpleServerMessageBuffer.AppendInteger(sh);

            owner.SendMessage(simpleServerMessageBuffer);
        }
    }
}