using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class RespectPetNotificationComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("PetRespectNotificationMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(pet.VirtualId);
            pet.SerializeInventory(simpleServerMessageBuffer);
            pet.Room.SendMessage(simpleServerMessageBuffer);
        }
    }
}