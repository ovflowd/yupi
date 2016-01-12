using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Pets.Structs;

namespace Yupi.Game.Pets
{
    internal class PetTypeManager
    {
        /// <summary>
        ///     The _pet races
        /// </summary>
        private static List<PetRace> _petRaces;

        /// <summary>
        ///     The _pet types
        /// </summary>
        private static Dictionary<string, PetType> _petTypes;

        public static void Init(IQueryAdapter dbClient)
        {
            GetRaces(dbClient);
            GetTypes(dbClient);
        }

        internal static void GetTypes(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM catalog_pets ORDER BY pet_race_id ASC");

            DataTable table = dbClient.GetTable();

            _petTypes = new Dictionary<string, PetType>();

            foreach (DataRow dataRow in table.Rows)
                _petTypes.Add((string) dataRow["pet_type"], new PetType(dataRow));
        }

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal static void GetRaces(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM pets_races ORDER BY race_type ASC");

            DataTable table = dbClient.GetTable();

            _petRaces = new List<PetRace>();

            foreach (DataRow dataRow in table.Rows)
                _petRaces.Add(new PetRace(dataRow));
        }

        /// <summary>
        ///     Gets the races for race identifier.
        /// </summary>
        /// <param name="sRaceId">The s race identifier.</param>
        /// <returns>List&lt;PetRace&gt;.</returns>
        public static List<PetRace> GetRacesForRaceId(uint sRaceId)
            => _petRaces.Where(current => current.RaceId == sRaceId).ToList();

        /// <summary>
        ///     Races the got races.
        /// </summary>
        /// <param name="sRaceId">The s race identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool RaceGotRaces(uint sRaceId) => GetRacesForRaceId(sRaceId).Any();

        public static uint GetPetRaceIdByType(string petType)
            => _petTypes.FirstOrDefault(p => p.Key == petType).Value.PetRaceId;

        public static uint GetPetRaceByItemName(string itemName) => GetPetRaceIdByType(itemName);

        public static string GetPetTypeByHabboPetType(string habboPetType)
            =>
                _petTypes.FirstOrDefault(
                    p => p.Value.PetRaceId == uint.Parse(habboPetType.Replace("a0 pet", string.Empty)))
                    .Value.PetRaceName;

        public static string GetHabboPetType(string petType) => $"a0 pet{GetPetRaceByItemName(petType)}";

        public static List<PetRace> GetRacesByPetType(string petType)
            => _petTypes.FirstOrDefault(p => p.Key == petType).Value.PetRaceSchemas;

        public static bool ItemIsPet(string itemName) => _petTypes.ContainsKey(itemName);
    }
}