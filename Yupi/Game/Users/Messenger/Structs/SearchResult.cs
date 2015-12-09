using Yupi.Messages;

namespace Yupi.Game.Users.Messenger.Structs
{
    /// <summary>
    ///     Struct SearchResult
    /// </summary>
    internal struct SearchResult
    {
        /// <summary>
        ///     The user identifier
        /// </summary>
        internal uint UserId;

        /// <summary>
        ///     The user name
        /// </summary>
        internal string UserName;

        /// <summary>
        ///     The motto
        /// </summary>
        internal string Motto;

        /// <summary>
        ///     The look
        /// </summary>
        internal string Look;

        /// <summary>
        ///     The last online
        /// </summary>
        internal string LastOnline;

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
        internal void Searialize(ServerMessage reply)
        {
            reply.AppendInteger(UserId);
            reply.AppendString(UserName);
            reply.AppendString(Motto);
            reply.AppendBool(Yupi.GetGame().GetClientManager().GetClient(UserId) != null);
            reply.AppendBool(false);
            reply.AppendString(string.Empty);
            reply.AppendInteger(0);
            reply.AppendString(Look);
            reply.AppendString(LastOnline);
        }
    }
}