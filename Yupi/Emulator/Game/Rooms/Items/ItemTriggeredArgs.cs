using System;
using Yupi.Game.Items.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Rooms.Items
{
    /// <summary>
    ///     Class ItemTriggeredArgs.
    /// </summary>
    public class ItemTriggeredArgs : EventArgs
    {
        /// <summary>
        ///     The triggering item
        /// </summary>
        internal readonly RoomItem TriggeringItem;

        /// <summary>
        ///     The triggering user
        /// </summary>
        internal readonly RoomUser TriggeringUser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemTriggeredArgs" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="item">The item.</param>
        public ItemTriggeredArgs(RoomUser user, RoomItem item)
        {
            TriggeringUser = user;
            TriggeringItem = item;
        }
    }
}