using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Users.Badges.Models;
using Yupi.Game.Users.Data.Models;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Users.Badges
{
    /// <summary>
    ///     Class BadgeComponent.
    /// </summary>
    internal class UserBadgeManager
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserBadgeManager" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="data">The data.</param>
        internal UserBadgeManager(uint userId, UserData data)
        {
            BadgeList = new HybridDictionary();

            foreach (Badge current in data.Badges.Where(current => !BadgeList.Contains(current.Code)))
                BadgeList.Add(current.Code, current);

            _userId = userId;
        }

        /// <summary>
        ///     Gets the count.
        /// </summary>
        /// <value>The count.</value>
        internal int Count => BadgeList.Count;

        /// <summary>
        ///     Gets or sets the badge list.
        /// </summary>
        /// <value>The badge list.</value>
        internal HybridDictionary BadgeList { get; set; }

        /// <summary>
        ///     Gets the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns>Badge.</returns>
        internal Badge GetBadge(string badge) => BadgeList.Contains(badge) ? (Badge) BadgeList[badge] : null;

        /// <summary>
        ///     Determines whether the specified badge has badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns><c>true</c> if the specified badge has badge; otherwise, <c>false</c>.</returns>
        internal bool HasBadge(string badge) => BadgeList.Contains(badge);

        /// <summary>
        ///     Gives the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <param name="inDatabase">if set to <c>true</c> [in database].</param>
        /// <param name="session">The session.</param>
        /// <param name="wiredReward">if set to <c>true</c> [wired reward].</param>
        internal void GiveBadge(string badge, bool inDatabase, GameClient session, bool wiredReward = false)
        {
            if (wiredReward)
                session.SendMessage(SerializeBadgeReward(!HasBadge(badge)));

            if (HasBadge(badge))
                return;

            if (inDatabase)
            {
                using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    commitableQueryReactor.SetQuery(
                        string.Concat("INSERT INTO users_badges (user_id,badge_id,badge_slot) VALUES (", _userId,
                            ",@badge,", 0, ")"));

                    commitableQueryReactor.AddParameter("badge", badge);
                    commitableQueryReactor.RunQuery();
                }
            }

            BadgeList.Add(badge, new Badge(badge, 0));

            session.SendMessage(SerializeBadge(badge));
            session.SendMessage(Update(badge));
        }

        /// <summary>
        ///     Serializes the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeBadge(string badge)
        {
            ServerMessage serverMessage = new ServerMessage();

            serverMessage.Init(LibraryParser.OutgoingRequest("ReceiveBadgeMessageComposer"));
            serverMessage.AppendInteger(1);
            serverMessage.AppendString(badge);

            return serverMessage;
        }

        /// <summary>
        ///     Serializes the badge reward.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage SerializeBadgeReward(bool success)
        {
            ServerMessage serverMessage = new ServerMessage();

            serverMessage.Init(LibraryParser.OutgoingRequest("WiredRewardAlertMessageComposer"));
            serverMessage.AppendInteger(success ? 7 : 1);

            return serverMessage;
        }

        /// <summary>
        ///     Resets the slots.
        /// </summary>
        internal void ResetSlots()
        {
            foreach (Badge badge in BadgeList.Values)
                badge.Slot = 0;
        }

        /// <summary>
        ///     Removes the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <param name="session">The session.</param>
        internal void RemoveBadge(string badge, GameClient session)
        {
            if (!HasBadge(badge))
                return;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery("DELETE FROM users_badges WHERE badge_id = @badge AND user_id = " +
                                                _userId);

                commitableQueryReactor.AddParameter("badge", badge);
                commitableQueryReactor.RunQuery();
            }

            BadgeList.Remove(GetBadge(badge));
            session.SendMessage(Serialize());
        }

        /// <summary>
        ///     Updates the specified badge identifier.
        /// </summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage Update(string badgeId)
        {
            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("NewInventoryObjectMessageComposer"));

            serverMessage.AppendInteger(1);
            serverMessage.AppendInteger(4);
            serverMessage.AppendInteger(1);
            serverMessage.AppendString(badgeId);

            return serverMessage;
        }

        /// <summary>
        ///     Serializes this instance.
        /// </summary>
        /// <returns>ServerMessage.</returns>
        internal ServerMessage Serialize()
        {
            List<Badge> list = new List<Badge>();

            ServerMessage serverMessage = new ServerMessage(LibraryParser.OutgoingRequest("LoadBadgesWidgetMessageComposer"));
            serverMessage.AppendInteger(Count);

            foreach (Badge badge in BadgeList.Values)
            {
                serverMessage.AppendInteger(1);
                serverMessage.AppendString(badge.Code);

                if (badge.Slot > 0)
                    list.Add(badge);
            }

            serverMessage.AppendInteger(list.Count);

            foreach (Badge current in list)
            {
                serverMessage.AppendInteger(current.Slot);
                serverMessage.AppendString(current.Code);
            }

            return serverMessage;
        }
    }
}