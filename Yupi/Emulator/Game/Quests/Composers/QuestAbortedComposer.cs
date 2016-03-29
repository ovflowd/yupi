using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Quests.Composers
{
    /// <summary>
    ///     Class QuestAbortedComposer.
    /// </summary>
    internal class QuestAbortedComposer
    {
        /// <summary>
        ///     Composes this instance.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose()
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("QuestAbortedMessageComposer"));
            serverMessage.AppendBool(false);
            return serverMessage;
        }
    }
}