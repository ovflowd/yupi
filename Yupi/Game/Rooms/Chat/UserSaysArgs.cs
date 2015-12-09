using System;
using Yupi.Game.Rooms.User;

namespace Yupi.Game.Rooms.Chat
{
    /// <summary>
    ///     Class UserSaysArgs.
    /// </summary>
    public class UserSaysArgs : EventArgs
    {
        /// <summary>
        ///     The message
        /// </summary>
        internal readonly string Message;

        /// <summary>
        ///     The user
        /// </summary>
        internal readonly RoomUser User;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserSaysArgs" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        public UserSaysArgs(RoomUser user, string message)
        {
            User = user;
            Message = message;
        }
    }
}