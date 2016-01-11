using System.Collections.Generic;
using System.Data;

namespace Yupi.Game.Pets.Structs
{
    internal class PetType
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