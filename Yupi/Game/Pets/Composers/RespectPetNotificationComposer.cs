using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets.Composers
{
    internal class RespectPetNotificationComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("PetRespectNotificationMessageComposer"));
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(pet.VirtualId);
            pet.SerializeInventory(serverMessage);
            pet.Room.SendMessage(serverMessage);
        }
    }
}