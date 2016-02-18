using System.Collections.Generic;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pets.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class PetCommandPanelComposer
    {
        internal static void GenerateMessage(Pet pet, Dictionary<uint, PetCommand> totalPetCommands,
            Dictionary<uint, PetCommand> petCommands, GameClient owner)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PetTrainerPanelMessageComposer"));

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