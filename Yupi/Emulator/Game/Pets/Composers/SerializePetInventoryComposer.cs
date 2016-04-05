using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Pets.Composers
{
     class SerializePetInventoryComposer
    {
         static void GenerateMessage(Pet pet, SimpleServerMessageBuffer messageBuffer, bool levelAfterName = false)
        {
            messageBuffer.AppendInteger(pet.PetId);
            messageBuffer.AppendString(pet.Name);

            if (levelAfterName)
                messageBuffer.AppendInteger(pet.Level);

            messageBuffer.AppendInteger(pet.RaceId);
            messageBuffer.AppendInteger(pet.Race);
            messageBuffer.AppendString(pet.Type == "pet_monster" ? "ffffff" : pet.Color);
            messageBuffer.AppendInteger(pet.Type == "pet_monster" ? 0u : pet.RaceId);

            if (pet.Type == "pet_monster" && pet.MoplaBreed != null)
            {
                string[] array = pet.MoplaBreed.PlantData.Substring(12).Split(' ');
                string[] array2 = array;

                foreach (string s in array2)
                    messageBuffer.AppendInteger(int.Parse(s));

                messageBuffer.AppendInteger(pet.MoplaBreed.GrowingStatus);

                return;
            }

            messageBuffer.AppendInteger(0);
            messageBuffer.AppendInteger(0);
        }
    }
}