using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Users.Inventory
{
    /// <summary>
    ///     Class UserClothesManager.
    /// </summary>
     public class UserClothesManager
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     The clothing
        /// </summary>
     public List<string> Clothing;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserClothesManager" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
     public UserClothesManager(uint userId)
        {
            _userId = userId;

            Clothing = new List<string>();

            DataTable dTable;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("SELECT clothing FROM users_clothing WHERE userid = @userid");

                queryReactor.AddParameter("userid", _userId);
                dTable = queryReactor.GetTable();
            }

            foreach (DataRow dRow in dTable.Rows)
                Clothing.Add(dRow["clothing"].ToString());
        }

        /// <summary>
        ///     Adds the specified clothing.
        /// </summary>
        /// <param name="clothing">The clothing.</param>
     public void Add(string clothing)
        {
            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery(
                    "INSERT INTO users_clothing (userid,clothing) VALUES (@userid,@clothing)");

                queryReactor.AddParameter("userid", _userId);
                queryReactor.AddParameter("clothing", clothing);
                queryReactor.RunQuery();
            }

            Clothing.Add(clothing);
        }
    }
}