using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Pets.Composers
{
    internal class PetExperienceComposer
    {
        internal static void GenerateMessage(Pet pet, uint amount)
        {
            ServerMessage serverMessage = new ServerMessage(PacketLibraryManager.OutgoingRequest("AddPetExperienceMessageComposer"));

            serverMessage.AppendInteger(pet.PetId);
            serverMessage.AppendInteger(pet.VirtualId);
            serverMessage.AppendInteger(amount);
            pet.Room.SendMessage(serverMessage);
        }
    }
}