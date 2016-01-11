using Yupi.Messages;

namespace Yupi.Game.Pets.Composers
{
    internal class SerializePetInventoryComposer
    {
        internal static void GenerateMessage(Pet pet, ServerMessage message, bool levelAfterName = false)
        {
            message.AppendInteger(pet.PetId);
            message.AppendString(pet.Name);

            if (levelAfterName)
                message.AppendInteger(pet.Level);

            message.AppendInteger(pet.RaceId);
            message.AppendInteger(pet.Race);
            message.AppendString(pet.Type == "pet_monster" ? "ffffff" : pet.Color);
            message.AppendInteger(pet.Type == "pet_monster" ? 0u : pet.RaceId);

            if (pet.Type == "pet_monster" && pet.MoplaBreed != null)
            {
                string[] array = pet.MoplaBreed.PlantData.Substring(12).Split(' ');
                string[] array2 = array;

                foreach (string s in array2)
                    message.AppendInteger(int.Parse(s));

                message.AppendInteger(pet.MoplaBreed.GrowingStatus);

                return;
            }

            message.AppendInteger(0);
            message.AppendInteger(0);
        }
    }
}