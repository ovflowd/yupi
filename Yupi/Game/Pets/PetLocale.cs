using System.Collections.Generic;
using System.Data;
using Yupi.Data.Base.Sessions.Interfaces;

namespace Yupi.Game.Pets
{
    /// <summary>
    ///     Class PetLocale.
    /// </summary>
    internal class PetLocale
    {
        /// <summary>
        ///     The _values
        /// </summary>
        private static Dictionary<string, string[]> _values;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal static void Init(IQueryAdapter dbClient)
        {
            _values = new Dictionary<string, string[]>();

            dbClient.SetQuery("SELECT * FROM pets_speech");
            var table = dbClient.GetTable();

            foreach (DataRow dataRow in table.Rows)
                _values.Add(dataRow[0].ToString(), dataRow[1].ToString().Split(';'));
        }

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String[].</returns>
        internal static string[] GetValue(string key)
        {
            string[] result;

            return _values.TryGetValue(key, out result) ? result : new[] {key};
        }
    }
}