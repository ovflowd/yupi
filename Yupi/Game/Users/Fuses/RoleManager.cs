using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Core.Io;
using Yupi.Data.Base.Adapters.Interfaces;

namespace Yupi.Game.Users.Fuses
{
    /// <summary>
    ///     Class RoleManager.
    /// </summary>
    internal class RoleManager
    {
        /// <summary>
        ///     The _CMD rights
        /// </summary>
        private readonly Dictionary<string, string> _cmdRights;

        /// <summary>
        ///     The _rights
        /// </summary>
        private readonly Dictionary<string, uint> _rights;

        /// <summary>
        ///     The _sub rights
        /// </summary>
        private readonly Dictionary<string, int> _subRights;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RoleManager" /> class.
        /// </summary>
        internal RoleManager()
        {
            _rights = new Dictionary<string, uint>();
            _subRights = new Dictionary<string, int>();
            _cmdRights = new Dictionary<string, string>();
        }

        /// <summary>
        ///     Loads the rights.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void LoadRights(IQueryAdapter dbClient)
        {
            ClearRights();

            dbClient.SetQuery("SELECT * FROM server_fuses;");

            DataTable table = dbClient.GetTable();

            if (table == null)
                return;

            foreach (DataRow dataRow in table.Rows)
                if (!_cmdRights.ContainsKey(dataRow["command"].ToString()))
                    _cmdRights.Add(dataRow["command"].ToString(), dataRow["rank"].ToString());
                else
                    Writer.WriteLine($"Duplicate Fuse Command \"{dataRow[0]}\" found", "Yupi.Fuses");

            dbClient.SetQuery("SELECT * FROM server_fuserights");

            DataTable table2 = dbClient.GetTable();

            if (table2 == null)
                return;

            foreach (DataRow dataRow2 in table2.Rows)
                if ((int) dataRow2["min_sub"] > 0)
                    _subRights.Add(dataRow2["fuse"].ToString(), (int) dataRow2["min_sub"]);
                else if (!_rights.ContainsKey(dataRow2["fuse"].ToString()))
                    _rights.Add(dataRow2["fuse"].ToString(), (uint) dataRow2["min_rank"]);
        }

        /// <summary>
        ///     Ranks the got command.
        /// </summary>
        /// <param name="rankId">The rank identifier.</param>
        /// <param name="cmd">The command.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool RankGotCommand(uint rankId, string cmd)
        {
            if (!_cmdRights.ContainsKey(cmd))
                return false;

            if (!_cmdRights[cmd].Contains(";"))
                return rankId >= uint.Parse(_cmdRights[cmd]);

            string[] cmdranks = _cmdRights[cmd].Split(';');

            return cmdranks.Any(rank => rank.Contains(Convert.ToString(rankId))) ||
                   _cmdRights[cmd].Contains(Convert.ToString(rankId));
        }

        /// <summary>
        ///     Ranks the has right.
        /// </summary>
        /// <param name="rankId">The rank identifier.</param>
        /// <param name="fuse">The fuse.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool RankHasRight(uint rankId, string fuse) => ContainsRight(fuse) && rankId >= _rights[fuse];

        /// <summary>
        ///     Determines whether the specified sub has vip.
        /// </summary>
        /// <param name="sub">The sub.</param>
        /// <param name="fuse">The fuse.</param>
        /// <returns><c>true</c> if the specified sub has vip; otherwise, <c>false</c>.</returns>
        internal bool HasVip(int sub, string fuse) => _subRights.ContainsKey(fuse) && _subRights[fuse] == sub;

        /// <summary>
        ///     Gets the rights for rank.
        /// </summary>
        /// <param name="rankId">The rank identifier.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        internal List<string> GetRightsForRank(uint rankId)
        {
            List<string> list = new List<string>();

            foreach (KeyValuePair<string, uint> current in _rights.Where(current => rankId >= current.Value && !list.Contains(current.Key)))
                list.Add(current.Key);

            return list;
        }

        /// <summary>
        ///     Determines whether the specified right contains right.
        /// </summary>
        /// <param name="right">The right.</param>
        /// <returns><c>true</c> if the specified right contains right; otherwise, <c>false</c>.</returns>
        internal bool ContainsRight(string right) => _rights.ContainsKey(right);

        /// <summary>
        ///     Clears the rights.
        /// </summary>
        internal void ClearRights()
        {
            _rights.Clear();
            _cmdRights.Clear();
            _subRights.Clear();
        }
    }
}