using System.Collections.Generic;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Emulator.Game.Rooms.User.Trade
{
    /// <summary>
    ///     Class TradeUser.
    /// </summary>
     class TradeUser
    {
        /// <summary>
        ///     The _room identifier
        /// </summary>
        private readonly uint _roomId;

        /// <summary>
        ///     The offered items
        /// </summary>
         List<UserItem> OfferedItems;

        /// <summary>
        ///     The user identifier
        /// </summary>
         uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TradeUser" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roomId">The room identifier.</param>
         TradeUser(uint userId, uint roomId)
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
         bool HasAccepted { get; set; }

        /// <summary>
        ///     Gets the room user.
        /// </summary>
        /// <returns>RoomUser.</returns>
         RoomUser GetRoomUser()
        {
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(_roomId);
            return room?.GetRoomUserManager().GetRoomUserByHabbo(UserId);
        }

        /// <summary>
        ///     Gets the client.
        /// </summary>
        /// <returns>GameClient.</returns>
         GameClient GetClient()
        {
            return Yupi.GetGame().GetClientManager().GetClientByUserId(UserId);
        }
    }
}