namespace Yupi.Model.Domain
{
    /// <summary>
    ///     Struct SearchResult
    /// </summary>
     public struct SearchResult
    {
        /// <summary>
        ///     The user identifier
        /// </summary>
     public uint UserId;

        /// <summary>
        ///     The user name
        /// </summary>
     public string UserName;

        /// <summary>
        ///     The motto
        /// </summary>
     public string Motto;

        /// <summary>
        ///     The look
        /// </summary>
     public string Look;

        /// <summary>
        ///     The last online
        /// </summary>
     public string LastOnline;

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
		/*
        /// <summary>
        ///     Searializes the specified reply.
        /// </summary>
        /// <param name="reply">The reply.</param>
     public void Searialize(SimpleServerMessageBuffer reply)
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
        }*/
    }
}