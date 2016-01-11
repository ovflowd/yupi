using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Users.Messenger.Structs;

namespace Yupi.Game.Users.Messenger
{
    /// <summary>
    ///     Class SearchResultFactory.
    /// </summary>
    internal static class SearchResultFactory
    {
        /// <summary>
        ///     Gets the search result.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>List&lt;SearchResult&gt;.</returns>
        internal static List<SearchResult> GetSearchResult(string query)
        {
            DataTable table;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT id,username,motto,look,last_online FROM users WHERE username LIKE @query LIMIT 50");

                commitableQueryReactor.AddParameter("query", $"{query}%");

                table = commitableQueryReactor.GetTable();
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