using System.Collections.Generic;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pets.Structs;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class PetCommandPanelComposer
    {
        internal static void GenerateMessage(Pet pet, Dictionary<uint, PetCommand> totalPetCommands,
            Dictionary<uint, PetCommand> petCommands, GameClient owner)
        {
            ServerMessage serverMessage = new ServerMessage(PacketLibraryManager.OutgoingRequest("PetTrainerPanelMessageComposer"));

            serverMessage.AppendInteger(pet.PetId);

            serverMessage.AppendInteger(totalPetCommands.Count);

            foreach (uint sh in totalPetCommands.Keys)
                serverMessage.AppendInteger(sh);

            serverMessage.AppendInteger(petCommands.Count);

            foreach (uint sh in petCommands.Keys)
                serverMessage.AppendInteger(sh);

            owner.SendMessage(serverMessage);
        }
    }
}