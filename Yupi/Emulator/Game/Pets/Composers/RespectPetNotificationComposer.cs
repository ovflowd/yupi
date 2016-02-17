using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class RespectPetNotificationComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            ServerMessage serverMessage = new ServerMessage(PacketLibraryManager.OutgoingRequest("PetRespectNotificationMessageComposer"));
            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(pet.VirtualId);
            pet.SerializeInventory(serverMessage);
            pet.Room.SendMessage(serverMessage);
        }
    }
}