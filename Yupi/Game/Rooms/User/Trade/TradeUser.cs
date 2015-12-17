using System.Collections.Generic;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Items.Interfaces;

namespace Yupi.Game.Rooms.User.Trade
{
    /// <summary>
    ///     Class TradeUser.
    /// </summary>
    internal class TradeUser
    {
        /// <summary>
        ///     The _room identifier
        /// </summary>
        private readonly uint _roomId;

        /// <summary>
        ///     The offered items
        /// </summary>
        internal List<UserItem> OfferedItems;

        /// <summary>
        ///     The user identifier
        /// </summary>
        internal uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TradeUser" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        internal TradeUser(uint userId, uint roomId)
        {
            UserId = userId;
            _roomId = roomId;
            HasAccepted = false;
            OfferedItems = new List<UserItem>();
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has accepted.
        /// </summary>
        /// <value><c>true</c> if this instance has accepted; otherwise, <c>false</c>.</value>
        internal bool HasAccepted { get; set; }

        /// <summary>
        ///     Gets the room user.
        /// </summary>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetRoomUser()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(_roomId);
            return room?.GetRoomUserManager().GetRoomUserByHabbo(UserId);
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
        internal GameClient GetClient()
        {
            return Yupi.GetGame().GetClientManager().GetClientByUserId(UserId);
        }
    }
}