using Yupi.Emulator.Game.Rooms.User;
using System;

namespace Yupi.Emulator.Game.Rooms.Chat.Interfaces
{
    /// <summary>
    ///     Struct InvokedChatMessage
    /// </summary>
     public struct InvokedChatMessage : IDisposable
    {
        /// <summary>
        ///     The user
        /// </summary>
     public RoomUser User;

        /// <summary>
        ///     The message
        /// </summary>
     public string Message;

        /// <summary>
        ///     The shout
        /// </summary>
     public bool Shout;

        /// <summary>
        ///     The colour type
        /// </summary>
     public int ColourType;

        /// <summary>
        ///     The count
        /// </summary>
     public int Count;

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