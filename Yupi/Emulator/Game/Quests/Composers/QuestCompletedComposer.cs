using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Quests.Composers
{
    /// <summary>
    ///     Class QuestCompletedComposer.
    /// </summary>
    internal class QuestCompletedComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="quest">The quest.</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(GameClient session, Quest quest)
        {
            var amountOfQuestsInCategory = Yupi.GetGame().GetQuestManager().GetAmountOfQuestsInCategory(quest.Category);
            var i = quest == null ? amountOfQuestsInCategory : quest.Number;
            var i2 = quest == null ? 0 : session.GetHabbo().GetQuestProgress(quest.Id);
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("QuestCompletedMessageComposer"));
            serverMessage.AppendString(quest.Category);
            serverMessage.AppendInteger(i);
            serverMessage.AppendInteger(quest.Name.Contains("xmas2012") ? 1 : amountOfQuestsInCategory);
            serverMessage.AppendInteger(quest == null ? 3 : quest.RewardType);
            serverMessage.AppendInteger(quest == null ? 0 : quest.Id);
            serverMessage.AppendBool(quest != null && session.GetHabbo().CurrentQuestId == quest.Id);
            serverMessage.AppendString(quest == null ? string.Empty : quest.ActionName);
            serverMessage.AppendString(quest == null ? string.Empty : quest.DataBit);
            serverMessage.AppendInteger(quest == null ? 0 : quest.Reward);
            serverMessage.AppendString(quest == null ? string.Empty : quest.Name);
            serverMessage.AppendInteger(i2);
            serverMessage.AppendInteger(quest == null ? 0u : quest.GoalData);
            serverMessage.AppendInteger(quest == null ? 0 : quest.TimeUnlock);
            serverMessage.AppendString("");
            serverMessage.AppendString("");
            serverMessage.AppendBool(true);
            serverMessage.AppendBool(true);
            return serverMessage;
        }
    }
}