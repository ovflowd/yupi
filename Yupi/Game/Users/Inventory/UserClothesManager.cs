using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interfaces;
using Yupi.Messages;

namespace Yupi.Game.Users.Inventory
{
    /// <summary>
    ///     Class UserClothesManager.
    /// </summary>
    internal class UserClothesManager
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     The clothing
        /// </summary>
        internal List<string> Clothing;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserClothesManager" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        internal UserClothesManager(uint userId)
        {
            _userId = userId;

            Clothing = new List<string>();

            DataTable dTable;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("SELECT clothing FROM users_clothing WHERE userid = @userid");

                commitableQueryReactor.AddParameter("userid", _userId);
                dTable = commitableQueryReactor.GetTable();
            }

            foreach (DataRow dRow in dTable.Rows)
                Clothing.Add(dRow["clothing"].ToString());
        }

        /// <summary>
        ///     Adds the specified clothing.
        /// </summary>
        /// <param name="clothing">The clothing.</param>
        internal void Add(string clothing)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "INSERT INTO users_clothing (userid,clothing) VALUES (@userid,@clothing)");

                commitableQueryReactor.AddParameter("userid", _userId);
                commitableQueryReactor.AddParameter("clothing", clothing);
                commitableQueryReactor.RunQuery();
            }

            Clothing.Add(clothing);
        }

        /// <summary>
        ///     Serializes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void Serialize(ServerMessage message)
        {
            message.StartArray();

            foreach (
                ClothingItem item1 in
                    Clothing.Select(clothing1 => Yupi.GetGame().GetClothingManager().GetClothesInFurni(clothing1)))
            {
                foreach (int clothe in item1.Clothes)
                    message.AppendInteger(clothe);

                message.SaveArray();
            }

            message.EndArray();
            message.StartArray();

            foreach (
                ClothingItem item2 in
                    Clothing.Select(clothing2 => Yupi.GetGame().GetClothingManager().GetClothesInFurni(clothing2)))
            {
                foreach (int clothe in item2.Clothes)
                    message.AppendString(item2.ItemName);

                message.SaveArray();
            }

            message.EndArray();
        }
    }
}