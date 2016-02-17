using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Pets.Composers
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