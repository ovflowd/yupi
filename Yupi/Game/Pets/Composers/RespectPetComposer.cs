using Yupi.Game.Rooms;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets.Composers
{
    class RespectPetComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("RespectPetMessageComposer"));
            serverMessage.AppendInteger(pet.VirtualId);
            serverMessage.AppendBool(true);
            pet.Room.SendMessage(serverMessage);
        }
    }
}
