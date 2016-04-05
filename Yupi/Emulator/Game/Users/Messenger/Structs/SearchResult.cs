using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Users.Messenger.Structs
{
    /// <summary>
    ///     Struct SearchResult
    /// </summary>
     struct SearchResult
    {
        /// <summary>
        ///     The user identifier
        /// </summary>
         uint UserId;

        /// <summary>
        ///     The user name
        /// </summary>
         string UserName;

        /// <summary>
        ///     The motto
        /// </summary>
         string Motto;

        /// <summary>
        ///     The look
        /// </summary>
         string Look;

        /// <summary>
        ///     The last online
        /// </summary>
         string LastOnline;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SearchResult" /> struct.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="motto">The motto.</param>
        /// <param name="look">The look.</param>
        /// <param name="lastOnline">The last online.</param>
        public SearchResult(uint userId, string userName, string motto, string look, string lastOnline)
        {
            UserId = userId;
            UserName = userName;
            Motto = motto;
            Look = look;
            LastOnline = lastOnline;
        }

        /// <summary>
        ///     Searializes the specified reply.
        /// </summary>
        /// <param name="reply">The reply.</param>
         void Searialize(SimpleServerMessageBuffer reply)
        {
            reply.AppendInteger(UserId);
            reply.AppendString(UserName);
            reply.AppendString(Motto);
            reply.AppendBool(Yupi.GetGame().GetClientManager().GetClientByUserId(UserId) != null);
            reply.AppendBool(false);
            reply.AppendString(string.Empty);
            reply.AppendInteger(0);
            reply.AppendString(Look);
            reply.AppendString(LastOnline);
        }
    }
}