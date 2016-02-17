using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Quests.Composers
{
    /// <summary>
    ///     Class QuestStartedComposer.
    /// </summary>
    internal class QuestStartedComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="quest">The quest.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(GameClient session, Quest quest)
        {
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("QuestStartedMessageComposer"));
            QuestListComposer.SerializeQuest(serverMessage, session, quest, quest.Category);
            return serverMessage;
        }
    }
}