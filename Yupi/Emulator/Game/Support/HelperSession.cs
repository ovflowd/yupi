using System.Collections.Generic;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Support
{
    /// <summary>
    ///     Class HelperSession.
    /// </summary>
     public class HelperSession
    {
        /// <summary>
        ///     The chats
        /// </summary>
     public List<string> Chats;

        /// <summary>
        ///     The helper
        /// </summary>
     public GameClient Helper;

        /// <summary>
        ///     The requester
        /// </summary>
     public GameClient Requester;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HelperSession" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="requester">The requester.</param>
        /// <param name="question">The question.</param>
     public HelperSession(GameClient helper, GameClient requester, string question)
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
     public void Response(GameClient responseClient, string response)
        {
        }
    }
}