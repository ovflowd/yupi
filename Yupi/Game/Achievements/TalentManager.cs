using System.Collections.Generic;
using System.Data;
using System.Linq;
using Yupi.Data.Base.Sessions.Interfaces;
using Yupi.Game.Achievements.Structs;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Achievements
{
    /// <summary>
    ///     Class TalentManager.
    /// </summary>
    internal class TalentManager
    {
        /// <summary>
        ///     The talents
        /// </summary>
        internal Dictionary<int, Talent> Talents = new Dictionary<int, Talent>();

        /// <summary>
        ///     Initializes the specified database client.
        /// </summary>
        /// <param name="dbClient">The database client.</param>
        internal void Initialize(IQueryAdapter dbClient)
        {
            dbClient.SetQuery("SELECT * FROM talents_data ORDER BY `order_num` ASC");

            DataTable table = dbClient.GetTable();

            foreach (Talent talent in from DataRow dataRow in table.Rows select new Talent(
                (int)dataRow["id"], 
                (string)dataRow["type"], 
                (int)dataRow["parent_category"],
                (int)dataRow["level"], 
                (string)dataRow["achievement_group"], 
                (int)dataRow["achievement_level"],
                (string)dataRow["prize"], 
                (uint)dataRow["prize_baseitem"]))
                Talents.Add(talent.Id, talent);
        }

        /// <summary>
        ///     Gets the talent.
        /// </summary>
        /// <param name="talentId">The talent identifier.</param>
        /// <returns>Talent.</returns>
        internal Talent GetTalent(int talentId) => Talents[talentId];

        /// <summary>
        ///     Levels the is completed.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="trackType">Type of the track.</param>
        /// <param name="talentLevel">The talent level.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal bool LevelIsCompleted(GameClient session, string trackType, int talentLevel) => 
            GetTalents(trackType, talentLevel).All(
                current =>
                (!session.GetHabbo().Achievements.ContainsKey(current.AchievementGroup)) || 
                session.GetHabbo().GetAchievementData(current.AchievementGroup).Level < current.AchievementLevel);

        /// <summary>
        ///     Completes the user talent.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="talent">The talent.</param>
        internal void CompleteUserTalent(GameClient session, Talent talent)
        {
            if (session?.GetHabbo() == null || session.GetHabbo().CurrentTalentLevel < talent.Level || session.GetHabbo().Talents.ContainsKey(talent.Id))
                return;

            if (!LevelIsCompleted(session, talent.Type, talent.Level))
                return;

            if (!string.IsNullOrEmpty(talent.Prize) && talent.PrizeBaseItem > 0u)
                Yupi.GetGame().GetCatalog().DeliverItems(session, Yupi.GetGame().GetItemManager().GetItem(talent.PrizeBaseItem), 1, string.Empty, 0, 0, string.Empty);

            session.GetHabbo().Talents.Add(talent.Id, new UserTalent(talent.Id, 1));

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"REPLACE INTO users_talents VALUES ('{session.GetHabbo().Id}', '{talent.Id}', '1');");

            var serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("TalentLevelUpMessageComposer"));

            serverMessage.AppendString(talent.Type);
            serverMessage.AppendInteger(talent.Level);
            serverMessage.AppendInteger(0);

            if (talent.Type == "citizenship" && talent.Level == 4)
            {
                serverMessage.AppendInteger(2);
                serverMessage.AppendString("HABBO_CLUB_VIP_7_DAYS");
                serverMessage.AppendInteger(7);
                serverMessage.AppendString(talent.Prize);
                serverMessage.AppendInteger(0);
            }
            else
            {
                serverMessage.AppendInteger(1);
                serverMessage.AppendString(talent.Prize);
                serverMessage.AppendInteger(0);
            }

            session.SendMessage(serverMessage);

            if (talent.Type == "citizenship" && talent.Level == 3)
                Yupi.GetGame().GetAchievementManager().ProgressUserAchievement(session, "ACH_Citizenship", 1);
            else if (talent.Type == "citizenship" && talent.Level == 4)
            {
                session.GetHabbo().GetSubscriptionManager().AddSubscription(7);

                using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                    queryReactor.RunFastQuery($"UPDATE users SET talent_status = 'helper' WHERE id = '{session.GetHabbo().Id}'");
            }
        }

        /// <summary>
        ///     Tries the get talent.
        /// </summary>
        /// <param name="achGroup">The ach group.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        internal Talent GetTalentData(string achGroup)
        {
            foreach (Talent current in Talents.Values.Where(current => current.AchievementGroup == achGroup))
                return current;
                          
            return new Talent();
        }

        /// <summary>
        ///     Gets all talents.
        /// </summary>
        /// <returns>Dictionary&lt;System.Int32, Talent&gt;.</returns>
        internal Dictionary<int, Talent> GetAllTalents() => Talents;

        /// <summary>
        ///     Gets the talents.
        /// </summary>
        /// <param name="trackType">Type of the track.</param>
        /// <param name="parentCategory">The parent category.</param>
        /// <returns>List&lt;Talent&gt;.</returns>
        internal List<Talent> GetTalents(string trackType, int parentCategory) => Talents.Values.Where(current => current.Type == trackType && current.ParentCategory == parentCategory).ToList();
    }
}