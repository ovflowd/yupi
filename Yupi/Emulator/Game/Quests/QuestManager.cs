using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Sessions.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Quests.Composers;
using Yupi.Messages;

namespace Yupi.Game.Quests
{
    /// <summary>
    ///     Class QuestManager.
    /// </summary>
    internal class QuestManager
    {
        /// <summary>
        ///     The _count
        /// </summary>
        private Dictionary<string, int> _count;

        /// <summary>
        ///     The _quests
        /// </summary>
        private Dictionary<int, Quest> _quests;

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        public void Initialize(IQueryAdapter dbClient)
        {
            _quests = new Dictionary<int, Quest>();
            _count = new Dictionary<string, int>();
            ReloadQuests(dbClient);
        }

        /// <summary>
        ///     Reloads the quests.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        public void ReloadQuests(IQueryAdapter dbClient)
        {
            _quests.Clear();
            dbClient.SetQuery("SELECT * FROM users_quests");
            var table = dbClient.GetTable();
            foreach (DataRow dataRow in table.Rows)
            {
                int id = Convert.ToInt32(dataRow["id"]);
                var category = (string) dataRow["type"];
                var number = (int) dataRow["level_num"];
                var goalType = (int) dataRow["goal_type"];
                var goalData = Convert.ToUInt32(dataRow["goal_data"]);
                var name = (string) dataRow["action"];
                var reward = (int) dataRow["pixel_reward"];
                var dataBit = (string) dataRow["data_bit"];
                var rewardType = Convert.ToInt32(dataRow["reward_type"].ToString());
                var timeUnlock = (int) dataRow["timestamp_unlock"];
                var timeLock = (int) dataRow["timestamp_lock"];
                var value = new Quest(id, category, number, (QuestType) goalType, goalData, name, reward, dataBit,
                    rewardType, timeUnlock, timeLock);
                _quests.Add(id, value);
                AddToCounter(category);
            }
        }

        /// <summary>
        ///     Gets the quests.
        /// </summary>
        /// <returns>ICollection&lt;Quest&gt;.</returns>
        public ICollection<Quest> GetQuests()
        {
            return _quests.Values;
        }

        /// <summary>
        ///     Gets the quest.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Quest.</returns>
        internal Quest GetQuest(int id)
        {
            Quest result;
            _quests.TryGetValue(id, out result);
            return result;
        }

        /// <summary>
        ///     Gets the amount of quests in category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>System.Int32.</returns>
        internal int GetAmountOfQuestsInCategory(string category)
        {
            int result;
            _count.TryGetValue(category, out result);
            return result;
        }

        /// <summary>
        ///     Progresses the user quest.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="questType">Type of the quest.</param>
        /// <param name="eventData">The event data.</param>
        internal void ProgressUserQuest(GameClient session, QuestType questType, uint eventData = 0u)
        {
            if (session == null || session.GetHabbo() == null || session.GetHabbo().CurrentQuestId <= 0u)
                return;
            var quest = GetQuest(session.GetHabbo().CurrentQuestId);
            if (quest == null || quest.GoalType != questType)
                return;
            var questProgress = session.GetHabbo().GetQuestProgress(quest.Id);
            var num = questProgress;
            var flag = false;

            {
                if (questType != QuestType.ExploreFindItem)
                    switch (questType)
                    {
                        case QuestType.StandOn:
                        case QuestType.GiveItem:
                            if (eventData != quest.GoalData)
                                return;
                            num = (int) quest.GoalData;
                            flag = true;
                            goto IL_DC;
                        case QuestType.GiveCoffee:
                        case QuestType.WaveReindeer:
                        case QuestType.XmasParty:
                        case QuestType.FurniMove:
                            num++;
                            if (num >= quest.GoalData)
                                flag = true;
                            goto IL_DC;
                    }
                if (eventData != quest.GoalData)
                    return;
                num = (int) quest.GoalData;
                flag = true;
                IL_DC:

                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.RunFastQuery(string.Concat("UPDATE users_quests_data SET progress = ", num,
                        " WHERE user_id = ", session.GetHabbo().Id, " AND quest_id =  ", quest.Id));
                    if (flag)
                        queryReactor.RunFastQuery(
                            $"UPDATE users_stats SET quest_id = 0 WHERE id = {session.GetHabbo().Id}");
                }

                session.GetHabbo().Quests[session.GetHabbo().CurrentQuestId] = num;
                session.SendMessage(QuestStartedComposer.Compose(session, quest));

                if (!flag)
                    return;
                session.GetHabbo().CurrentQuestId = 0;
                session.GetHabbo().LastQuestCompleted = quest.Id;
                session.SendMessage(QuestCompletedComposer.Compose(session, quest));
                session.GetHabbo().ActivityPoints += quest.Reward;
                session.GetHabbo().NotifyNewPixels(quest.Reward);
                session.GetHabbo().UpdateSeasonalCurrencyBalance();
                GetList(session, null);
            }
        }

        /// <summary>
        ///     Gets the next quest in series.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="number">The number.</param>
        /// <returns>Quest.</returns>
        internal Quest GetNextQuestInSeries(string category, int number)
        {
            return _quests.Values.FirstOrDefault(current => current.Category == category && current.Number == number);
        }

        /// <summary>
        ///     Gets the seasonal quests.
        /// </summary>
        /// <param name="season">The season.</param>
        /// <returns>List&lt;Quest&gt;.</returns>
        internal List<Quest> GetSeasonalQuests(string season)
        {
            return
                _quests.Values.Where(
                    current => current.Category.Contains(season) && current.TimeUnlock - Yupi.GetUnixTimeStamp() < 0)
                    .ToList();
        }

        /// <summary>
        ///     Gets the list.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">The message.</param>
        internal void GetList(GameClient session, ClientMessage message)
        {
            session.SendMessage(QuestListComposer.Compose(session, _quests.Values.ToList(), message != null));
        }

        /// <summary>
        ///     Activates the quest.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">The message.</param>
        internal void ActivateQuest(GameClient session, ClientMessage message)
        {
            var quest = GetQuest(message.GetInteger());
            if (quest == null)
                return;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery(string.Concat("REPLACE INTO users_quests_data(user_id,quest_id) VALUES (",
                    session.GetHabbo().Id, ", ", quest.Id, ")"));
                queryReactor.RunFastQuery(string.Concat("UPDATE users_stats SET quest_id = ", quest.Id, " WHERE id = ",
                    session.GetHabbo().Id));
            }
            session.GetHabbo().CurrentQuestId = quest.Id;
            GetList(session, null);
            session.SendMessage(QuestStartedComposer.Compose(session, quest));
        }

        /// <summary>
        ///     Gets the current quest.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">The message.</param>
        internal void GetCurrentQuest(GameClient session, ClientMessage message)
        {
            if (!session.GetHabbo().InRoom)
                return;
            var quest = GetQuest(session.GetHabbo().LastQuestCompleted);
            var nextQuestInSeries = GetNextQuestInSeries(quest.Category, quest.Number + 1);

            if (nextQuestInSeries == null)
                return;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.RunFastQuery(string.Concat("REPLACE INTO users_quests_data(user_id,quest_id) VALUES (",
                    session.GetHabbo().Id, ", ", nextQuestInSeries.Id, ")"));
                queryReactor.RunFastQuery(string.Concat("UPDATE users_stats SET quest_id = ", nextQuestInSeries.Id,
                    " WHERE id = ", session.GetHabbo().Id));
            }
            session.GetHabbo().CurrentQuestId = nextQuestInSeries.Id;
            GetList(session, null);
            session.SendMessage(QuestStartedComposer.Compose(session, nextQuestInSeries));
        }

        /// <summary>
        ///     Cancels the quest.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">The message.</param>
        internal void CancelQuest(GameClient session, ClientMessage message)
        {
            var quest = GetQuest(session.GetHabbo().CurrentQuestId);
            if (quest == null)
                return;
            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(string.Concat("DELETE FROM users_quests_data WHERE user_id = ",
                    session.GetHabbo().Id, " AND quest_id = ", quest.Id, ";UPDATE users_stats SET quest_id=0 WHERE id=",
                    session.GetHabbo().Id));
            session.GetHabbo().CurrentQuestId = 0;
            session.SendMessage(QuestAbortedComposer.Compose());
            GetList(session, null);
        }

        /// <summary>
        ///     Adds to counter.
        /// </summary>
        /// <param name="category">The category.</param>
        private void AddToCounter(string category)
        {
            int num;
            if (_count.TryGetValue(category, out num))
            {
                _count[category] = num + 1;

                return;
            }
            _count.Add(category, 1);
        }
    }
}