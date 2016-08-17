using System.Data;
using System.Globalization;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Groups.Structs;
using Yupi.Emulator.Game.Items.Interactions.Enums;


namespace Yupi.Emulator.Game.Items.Interfaces
{
    /// <summary>
    ///     Class UserItem.
    /// </summary>
     public class UserItem
    {
        /// <summary>
        ///     The base item
        /// </summary>
     public readonly Item BaseItem;

        /// <summary>
        ///     The extra data
        /// </summary>
     public string ExtraData;

        /// <summary>
        ///     The group identifier
        /// </summary>
     public uint GroupId;

        /// <summary>
        ///     The identifier
        /// </summary>
     public uint Id;

        /// <summary>
        ///     The is wall item
        /// </summary>
     public bool IsWallItem;

        /// <summary>
        ///     The limited sell identifier
        /// </summary>
     public uint LimitedSellId, LimitedStack;

        /// <summary>
        ///     The song code
        /// </summary>
     public string SongCode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserItem" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="baseName">The base item identifier.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="group">The group.</param>
        /// <param name="songCode">The song code.</param>
     public UserItem(uint id, string baseName, string extraData, uint group, string songCode)
        {
            Id = id;
            ExtraData = extraData;
            GroupId = group;

            BaseItem = Yupi.GetGame().GetItemManager().GetItemByName(baseName);

            if (BaseItem == null)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery($"SELECT * FROM items_limited WHERE item_id={id} LIMIT 1");
                DataRow row = queryReactor.GetRow();

                if (row != null)
                {
                    uint.TryParse(row[1].ToString(), out LimitedSellId);
                    uint.TryParse(row[2].ToString(), out LimitedStack);
                }
            }

            IsWallItem = BaseItem.Type == 'i';
            SongCode = songCode;
        }

   
       
    }
}