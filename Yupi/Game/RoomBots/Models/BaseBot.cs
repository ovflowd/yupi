using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.RoomBots.Interfaces;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.RoomBots.Models
{
    internal class BaseBot : BotAi
    {
        /// <summary>
        ///     Called when [self enter room].
        /// </summary>
        internal override void OnSelfEnterRoom()
        {
        }

        /// <summary>
        ///     Called when [self leave room].
        /// </summary>
        /// <param name="kicked">if set to <c>true</c> [kicked].</param>
        internal override void OnSelfLeaveRoom(bool kicked)
        {
        }

        /// <summary>
        ///     Called when [user enter room].
        /// </summary>
        /// <param name="user">The user.</param>
        internal override void OnUserEnterRoom(RoomUser user)
        {
        }

        /// <summary>
        ///     Called when [user leave room].
        /// </summary>
        /// <param name="client">The client.</param>
        internal override void OnUserLeaveRoom(GameClient client)
        {
        }

        /// <summary>
        ///     Called when [user say].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="msg">The MSG.</param>
        internal override void OnUserSay(RoomUser user, string msg)
        {
        }

        /// <summary>
        ///     Called when [user shout].
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        internal override void OnUserShout(RoomUser user, string message)
        {
        }

        /// <summary>
        ///     Called when [timer tick].
        /// </summary>
        internal override void OnTimerTick()
        {
        }

        internal override void OnChatTick()
        {
        }

        /// <summary>
        ///     Modifieds this instance.
        /// </summary>
        internal override void Modified()
        {
        }
    }
}
