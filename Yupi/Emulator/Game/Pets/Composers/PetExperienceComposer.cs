using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class PetExperienceComposer
    {
        internal static void GenerateMessage(Pet pet, uint amount)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("AddPetExperienceMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(pet.PetId);
            simpleServerMessageBuffer.AppendInteger(pet.VirtualId);
            simpleServerMessageBuffer.AppendInteger(amount);
            pet.Room.SendMessage(simpleServerMessageBuffer);
        }
    }
}