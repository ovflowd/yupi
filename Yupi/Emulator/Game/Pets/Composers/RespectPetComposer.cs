using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class RespectPetComposer
    {
        internal static void GenerateMessage(Pet pet)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RespectPetMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(pet.VirtualId);
            simpleServerMessageBuffer.AppendBool(true);
            pet.Room.SendMessage(simpleServerMessageBuffer);
        }
    }
}