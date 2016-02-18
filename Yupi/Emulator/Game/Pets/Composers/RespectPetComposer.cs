using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class RespectPetComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("RespectPetMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(pet.VirtualId);
            simpleServerMessageBuffer.AppendBool(true);
            pet.Room.SendMessage(simpleServerMessageBuffer);
        }
    }
}