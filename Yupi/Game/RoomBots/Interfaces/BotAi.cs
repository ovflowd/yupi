using System;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.RoomBots.Interfaces
{
    /// <summary>
    ///     Class BotAI.
    /// </summary>
    internal abstract class BotAi
    {
        /// <summary>
        ///     The _room
        /// </summary>
        private Room _room;

        /// <summary>
        ///     The _room user
        /// </summary>
        private RoomUser _roomUser;

        /// <summary>
        ///     The base identifier
        /// </summary>
        internal uint BaseId;

        /// <summary>
        ///     Initializes the specified base identifier.
        /// </summary>
        /// <param name="baseId">The base identifier.</param>
        /// <param name="roomUserId">The room user identifier.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="user">The user.</param>
        /// <param name="room">The room.</param>
        internal void Init(uint baseId, int roomUserId, uint roomId, RoomUser user, Room room)
        {
            BaseId = baseId;
            _roomUser = user;
            _room = room;
        }

        /// <summary>
        ///     Gets the room.
        /// </summary>
        /// <returns>Room.</returns>
        internal Room GetRoom() => _room;

        /// <summary>
        ///     Gets the room user.
        /// </summary>
        /// <returns>RoomUser.</returns>
        internal RoomUser GetRoomUser() => _roomUser;

        /// <summary>
        ///     Gets the bot data.
        /// </summary>
        /// <returns>RoomBot.</returns>
        internal RoomBot GetBotData()
        {
            return GetRoomUser() == null ? null : GetRoomUser().BotData;
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        internal void Dispose()
        {
            _room = null;
            _roomUser = null;

            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Called when [self enter room].
        /// </summary>
        internal abstract void OnSelfEnterRoom();

        /// <summary>
        ///     Called when [self leave room].
        /// </summary>
        /// <param name="kicked">if set to <c>true</c> [kicked].</param>
        internal abstract void OnSelfLeaveRoom(bool kicked);

        /// <summary>
        ///     Called when [user enter room].
        /// </summary>
        /// <param name="user">The user.</param>
        internal abstract void OnUserEnterRoom(RoomUser user);

        /// <summary>
        ///     Called when [user leave room].
        /// </summary>
        /// <param name="client">The client.</param>
        internal abstract void OnUserLeaveRoom(GameClient client);

        /// <summary>
        ///     Called when [user say].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="msg">The MSG.</param>
        internal abstract void OnUserSay(RoomUser user, string msg);

        /// <summary>
        ///     Called when [user shout].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        internal abstract void OnUserShout(RoomUser user, string message);

        /// <summary>
        ///     Called when [timer tick].
        /// </summary>
        internal abstract void OnTimerTick();

        internal abstract void OnChatTick();

        /// <summary>
        ///     Modifieds this instance.
        /// </summary>
        internal abstract void Modified();
    }
}