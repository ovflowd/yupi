using System.Collections.Generic;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Pets.Structs;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets.Composers
{
    internal class PetCommandPanelComposer
    {
        internal static void GenerateMessage(Pet pet, Dictionary<uint, PetCommand> totalPetCommands,
            Dictionary<uint, PetCommand> petCommands, GameClient owner)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetTrainerPanelMessageComposer"));

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