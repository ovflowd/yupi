using Yupi.Messages;

namespace Yupi.Game.Users.Messenger.Structs
{
    /// <summary>
    ///     Class MessengerRequest.
    /// </summary>
    internal class MessengerRequest
    {
        /// <summary>
        ///     The _user look
        /// </summary>
        private readonly string _look;

        /// <summary>
        ///     The _user name
        /// </summary>
        private readonly string _userName;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessengerRequest" /> class.
        /// </summary>
        /// <param name="toUser">To user.</param>
        /// <param name="fromUser">From user.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="look"></param>
        internal MessengerRequest(uint toUser, uint fromUser, string userName, string look)
        {
            To = toUser;
            From = fromUser;
            _userName = userName;
            _look = look;
        }

        /// <summary>
        ///     Gets to.
        /// </summary>
        /// <value>To.</value>
        internal uint To { get; private set; }

        /// <summary>
        ///     Gets from.
        /// </summary>
        /// <value>From.</value>
        internal uint From { get; }

        /// <summary>
        ///     Serializes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        internal void Serialize(ServerMessage request)
        {
            request.AppendInteger(From);
            request.AppendString(_userName);
            request.AppendString(_look);
        }
    }
}