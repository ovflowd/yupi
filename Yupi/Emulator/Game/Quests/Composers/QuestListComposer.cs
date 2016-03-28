using System.Collections.Generic;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Quests.Composers
{
    /// <summary>
    ///     Class QuestListComposer.
    /// </summary>
    internal class QuestListComposer
    {
        /// <summary>
        ///     Composes the specified session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="quests">The quests.</param>
        /// <param name="send">if set to <c>true</c> [send].</param>
        /// <returns>ServerMessage.</returns>
        internal static ServerMessage Compose(GameClient session, List<Quest> quests, bool send)
        {
            var dictionary = new Dictionary<string, int>();
            var dictionary2 = new Dictionary<string, Quest>();
            foreach (var current in quests)
            {
                if (!current.Category.Contains("xmas2012"))
                {
                    if (!dictionary.ContainsKey(current.Category))
                    {
                        dictionary.Add(current.Category, 1);
                        dictionary2.Add(current.Category, null);
                    }
                    if (current.Number >= dictionary[current.Category])
                    {
                        var questProgress = session.GetHabbo().GetQuestProgress(current.Id);
                        if (session.GetHabbo().CurrentQuestId != current.Id && questProgress >= current.GoalData)
                        {
                            dictionary[current.Category] = current.Number + 1;
                        }
                    }
                }
            }
            foreach (var current2 in quests)
            {
                foreach (var current3 in dictionary)
                {
                    if (!current2.Category.Contains("xmas2012") && current2.Category == current3.Key &&
                        current2.Number == current3.Value)
                    {
                        dictionary2[current3.Key] = current2;
                        break;
                    }
                }
            }
            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("QuestListMessageComposer"));
            serverMessage.AppendInteger(dictionary2.Count);
            foreach (var current4 in dictionary2)
            {
                if (current4.Value != null)
                {
                    SerializeQuest(serverMessage, session, current4.Value, current4.Key);
                }
            }
            foreach (var current5 in dictionary2)
            {
                if (current5.Value == null)
                {
                    SerializeQuest(serverMessage, session, current5.Value, current5.Key);
                }
            }
            serverMessage.AppendBool(send);
            return serverMessage;
        }

        /// <summary>
        ///     Serializes the quest.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="session">The session.</param>
        /// <param name="quest">The quest.</param>
        /// <param name="category">The category.</param>
        internal static void SerializeQuest(ServerMessage message, GameClient session, Quest quest, string category)
        {
            if (message == null || session == null)
            {
                return;
            }
            var amountOfQuestsInCategory = Yupi.GetGame().GetQuestManager().GetAmountOfQuestsInCategory(category);

            {
                var num = quest == null ? amountOfQuestsInCategory : quest.Number - 1;
                var num2 = quest == null ? 0 : session.GetHabbo().GetQuestProgress(quest.Id);
                if (quest != null && quest.IsCompleted(num2))
                {
                    num++;
                }
                message.AppendString(category);
                message.AppendInteger(quest == null ? 0 : (quest.Category.Contains("xmas2012") ? 0 : num));
                message.AppendInteger(quest == null
                    ? 0
                    : (quest.Category.Contains("xmas2012") ? 0 : amountOfQuestsInCategory));
                message.AppendInteger(quest?.RewardType ?? 3);
                message.AppendInteger(quest?.Id ?? 0);
                message.AppendBool(quest != null && session.GetHabbo().CurrentQuestId == quest.Id);
                message.AppendString(quest == null ? string.Empty : quest.ActionName);
                message.AppendString(quest == null ? string.Empty : quest.DataBit);
                message.AppendInteger(quest?.Reward ?? 0);
                message.AppendString(quest == null ? string.Empty : quest.Name);
                message.AppendInteger(num2);
                message.AppendInteger(quest?.GoalData ?? 0u);
                message.AppendInteger(quest?.TimeUnlock ?? 0);
                message.AppendString(string.Empty);
                message.AppendString(string.Empty);
                message.AppendBool(true);
            }
        }
    }
}