using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Users.Messenger.Structs;

namespace Yupi.Emulator.Game.Users.Messenger
{
    /// <summary>
    ///     Class SearchResultFactory.
    /// </summary>
     public static class SearchResultFactory
    {
        /// <summary>
        ///     Gets the search result.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>List&lt;SearchResult&gt;.</returns>
     public static List<SearchResult> GetSearchResult(string query)
        {
            DataTable table;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "SELECT id,username,motto,look,last_online FROM users WHERE username LIKE @query LIMIT 50");

                queryReactor.AddParameter("query", $"{query}%");

                table = queryReactor.GetTable();
            }

            return (from DataRow dataRow in table.Rows
                let userId = Convert.ToUInt32(dataRow[0])
                let userName = (string) dataRow[1]
                let motto = (string) dataRow[2]
                let look = (string) dataRow[3]
                let lastOnline = dataRow[4].ToString()
                select new SearchResult(userId, userName, motto, look, lastOnline)).ToList();
        }
    }
}