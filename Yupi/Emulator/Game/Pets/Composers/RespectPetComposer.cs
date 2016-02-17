using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class RespectPetComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            ServerMessage serverMessage = new ServerMessage(PacketLibraryManager.OutgoingRequest("RespectPetMessageComposer"));
            serverMessage.AppendInteger(pet.VirtualId);
            serverMessage.AppendBool(true);
            pet.Room.SendMessage(serverMessage);
        }
    }
}