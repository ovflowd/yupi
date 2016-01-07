using System.Collections.Generic;
using System.Data;

namespace Yupi.Game.Pets.Structs
{
    internal class PetType
    {
        public string PetRaceName;

        public uint PetRaceId;

        public List<PetRace> PetRaceSchemas;

        public PetType(DataRow row)
        {
            PetRaceName = (string)row["pet_type"];
            PetRaceId = (uint)row["pet_race_id"];
            PetRaceSchemas = PetTypeManager.GetRacesForRaceId((uint)row["pet_race_id"]);
        }
    }
}
