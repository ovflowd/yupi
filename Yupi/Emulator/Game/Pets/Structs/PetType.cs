using System.Collections.Generic;
using System.Data;

namespace Yupi.Emulator.Game.Pets.Structs
{
     public class PetType
    {
        public uint PetRaceId;

        public string PetRaceName;

        public List<PetRace> PetRaceSchemas;

        public PetType(DataRow row)
        {
            PetRaceName = (string) row["pet_type"];
            PetRaceId = (uint) row["pet_race_id"];
            PetRaceSchemas = PetTypeManager.GetRacesForRaceId((uint) row["pet_race_id"]);
        }
    }
}