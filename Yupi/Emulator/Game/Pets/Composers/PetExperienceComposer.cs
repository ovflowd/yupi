using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Pets.Composers
{
     public class PetExperienceComposer
    {
     public static void GenerateMessage(Pet pet, uint amount)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("AddPetExperienceMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(pet.PetId);
            simpleServerMessageBuffer.AppendInteger(pet.VirtualId);
            simpleServerMessageBuffer.AppendInteger(amount);
            pet.Room.SendMessage(simpleServerMessageBuffer);
        }
    }
}