using System.Collections.Generic;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Support
{
    /// <summary>
    ///     Class HelperSession.
    /// </summary>
    internal class HelperSession
    {
        /// <summary>
        ///     The chats
        /// </summary>
        internal List<string> Chats;

        /// <summary>
        ///     The helper
        /// </summary>
        internal GameClient Helper;

        /// <summary>
        ///     The requester
        /// </summary>
        internal GameClient Requester;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelperSession" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="requester">The requester.</param>
        /// <param name="question">The question.</param>
        internal HelperSession(GameClient helper, GameClient requester, string question)
        {
            Helper = helper;
            Requester = requester;
            Chats = new List<string> {question};
            Response(requester, question);
        }

        /// <summary>
        ///     Responses the specified response client.
        /// </summary>
        /// <param name="responseClient">The response client.</param>
        /// <param name="response">The response.</param>
        internal void Response(GameClient responseClient, string response)
        {
        }
    }
}