using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.SoundMachine.Songs
{
    /// <summary>
    ///     Class SongItem.
    /// </summary>
    internal class SongItem
    {
        /// <summary>
        ///     The base item
        /// </summary>
        internal Item BaseItem;

        /// <summary>
        ///     The extra data
        /// </summary>
        internal string ExtraData;

        /// <summary>
        ///     The item identifier
        /// </summary>
        internal uint ItemId;

        /// <summary>
        ///     The song code
        /// </summary>
        internal string SongCode;

        /// <summary>
        ///     The song identifier
        /// </summary>
        internal uint SongId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SongItem" /> class.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <param name="songId">The song identifier.</param>
        /// <param name="baseName">The base item.</param>
        /// <param name="extraData">The extra data.</param>
        /// <param name="songCode">The song code.</param>
        public SongItem(uint itemId, uint songId, string baseName, string extraData, string songCode)
        {
            ItemId = itemId;
            SongId = songId;

            BaseItem = Yupi.GetGame().GetItemManager().GetItemByName(baseName);

            ExtraData = extraData;
            SongCode = songCode;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SongItem" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public SongItem(UserItem item)
        {
            ItemId = item.Id;
            SongId = SoundMachineSongManager.GetSongId(item.SongCode);
            BaseItem = item.BaseItem;
            ExtraData = item.ExtraData;
            SongCode = item.SongCode;
        }

        /// <summary>
        ///     Saves to database.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        internal void SaveToDatabase(uint roomId)
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    $"REPLACE INTO items_songs VALUES ('{ItemId}', '{roomId}', '{SongId}')");
        }

        /// <summary>
        ///     Removes from database.
        /// </summary>
        internal void RemoveFromDatabase()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery($"DELETE FROM items_songs WHERE itemid = '{ItemId}'");
        }
    }
}