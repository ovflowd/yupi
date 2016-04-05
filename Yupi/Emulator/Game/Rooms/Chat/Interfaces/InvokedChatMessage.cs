using Yupi.Emulator.Game.Rooms.User;
using System;

namespace Yupi.Emulator.Game.Rooms.Chat.Interfaces
{
    /// <summary>
    ///     Struct InvokedChatMessage
    /// </summary>
     struct InvokedChatMessage : IDisposable
    {
        /// <summary>
        ///     The user
        /// </summary>
         RoomUser User;

        /// <summary>
        ///     The message
        /// </summary>
         string Message;

        /// <summary>
        ///     The shout
        /// </summary>
         bool Shout;

        /// <summary>
        ///     The colour type
        /// </summary>
         int ColourType;

        /// <summary>
        ///     The count
        /// </summary>
         int Count;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InvokedChatMessage" /> struct.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        /// <param name="shout">if set to <c>true</c> [shout].</param>
        /// <param name="colour">The colour.</param>
        /// <param name="count">The count.</param>
        public InvokedChatMessage(RoomUser user, string message, bool shout, int colour, int count)
        {
            User = user;
            Message = message;
            Shout = shout;
            ColourType = colour;
            Count = count;
        }

        /// <summary>
        ///     Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            User = null;
            Message = null;
			// TODO Make user and message disposable???
        }
    }
}