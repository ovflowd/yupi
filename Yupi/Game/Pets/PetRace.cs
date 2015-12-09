using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class PetRace.
    /// </summary>
    public class PetRace
    {
        /// <summary>
        ///     The races
        /// </summary>
        public static List<PetRace> Races;

        /// <summary>
        ///     The color1
        /// </summary>
        public int Color1;

        /// <summary>
        ///     The color2
        /// </summary>
        public int Color2;

        /// <summary>
        ///     The has1 color
        /// </summary>
        public bool Has1Color;

        /// <summary>
        ///     The has2 color
        /// </summary>
        public bool Has2Color;

        /// <summary>
        ///     The race identifier
        /// </summary>
        public int RaceId;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        public static void Init(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM pets_breeds");

            var table = dbClient.GetTable();
            Races = new List<PetRace>();

            foreach (var item in from DataRow row in table.Rows select new PetRace { RaceId = (int) row["breed_id"], Color1 = (int) row["color1"], Color2 = (int) row["color2"], Has1Color = ((string) row["color1_enabled"]) == "1", Has2Color = ((string) row["color2_enabled"]) == "1" })
                Races.Add(item);
        }

        /// <summary>
        ///     Gets the races for race identifier.
        /// </summary>
        /// <param name="sRaceId">The s race identifier.</param>
        /// <returns>List&lt;PetRace&gt;.</returns>
        public static List<PetRace> GetRacesForRaceId(int sRaceId) => Races.Where(current => current.RaceId == sRaceId).ToList();

        /// <summary>
        ///     Races the got races.
        /// </summary>
        /// <param name="sRaceId">The s race identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool RaceGotRaces(int sRaceId) => GetRacesForRaceId(sRaceId).Any();

        /// <summary>
        ///     Gets the pet identifier.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="packet">The packet.</param>
        /// <returns>System.Int32.</returns>
        public static int GetPetId(string type, out string packet)
        {
            int petId;

            int.TryParse(type.Replace("a0 pet", string.Empty).ToString(CultureInfo.InvariantCulture), out petId);

            packet = type;

            if (petId >= 0 && petId <= 26)
                return petId;

            return petId;
        }
    }
}