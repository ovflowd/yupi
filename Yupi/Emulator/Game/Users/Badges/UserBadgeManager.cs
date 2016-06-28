using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Users.Badges.Models;
using Yupi.Emulator.Game.Users.Data.Models;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Users.Badges
{
    /// <summary>
    ///     Class BadgeComponent.
    /// </summary>
     public class UserBadgeManager
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
     public UserBadgeManager(uint userId, UserData data)
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
     public int Count => BadgeList.Count;

        /// <summary>
        ///     Gets or sets the badge list.
        /// </summary>
        /// <value>The badge list.</value>
     public HybridDictionary BadgeList { get; set; }

        /// <summary>
        ///     Gets the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns>Badge.</returns>
     public Badge GetBadge(string badge) => BadgeList.Contains(badge) ? (Badge) BadgeList[badge] : null;

        /// <summary>
        ///     Determines whether the specified badge has badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns><c>true</c> if the specified badge has badge; otherwise, <c>false</c>.</returns>
     public bool HasBadge(string badge) => BadgeList.Contains(badge);

        /// <summary>
        ///     Gives the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <param name="inDatabase">if set to <c>true</c> [in database].</param>
        /// <param name="session">The session.</param>
        /// <param name="wiredReward">if set to <c>true</c> [wired reward].</param>
     public void GiveBadge(string badge, bool inDatabase, GameClient session, bool wiredReward = false)
        {
            if (wiredReward)
                session.SendMessage(SerializeBadgeReward(!HasBadge(badge)));

            if (HasBadge(badge))
                return;

            if (inDatabase)
            {
                using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                {
                    queryReactor.SetQuery(
                        string.Concat("INSERT INTO users_badges (user_id,badge_id,badge_slot) VALUES (", _userId,
                            ",@badge,", 0, ")"));

                    queryReactor.AddParameter("badge", badge);
                    queryReactor.RunQuery();
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
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer SerializeBadge(string badge)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();

            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("ReceiveBadgeMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendString(badge);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes the badge reward.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer SerializeBadgeReward(bool success)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer();

            simpleServerMessageBuffer.Init(PacketLibraryManager.OutgoingHandler("WiredRewardAlertMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(success ? 7 : 1);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Resets the slots.
        /// </summary>
     public void ResetSlots()
        {
            foreach (Badge badge in BadgeList.Values)
                badge.Slot = 0;
        }

        /// <summary>
        ///     Removes the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <param name="session">The session.</param>
     public void RemoveBadge(string badge, GameClient session)
        {
            if (!HasBadge(badge))
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                queryReactor.SetQuery("DELETE FROM users_badges WHERE badge_id = @badge AND user_id = " +
                                      _userId);

                queryReactor.AddParameter("badge", badge);
                queryReactor.RunQuery();
            }

            BadgeList.Remove(GetBadge(badge));
            session.SendMessage(Serialize());
        }

        /// <summary>
        ///     Updates the specified badge identifier.
        /// </summary>
        /// <param name="badgeId">The badge identifier.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer Update(string badgeId)
        {
            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("NewInventoryObjectMessageComposer"));

            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendInteger(4);
            simpleServerMessageBuffer.AppendInteger(1);
            simpleServerMessageBuffer.AppendString(badgeId);

            return simpleServerMessageBuffer;
        }

        /// <summary>
        ///     Serializes this instance.
        /// </summary>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer Serialize()
        {
            List<Badge> list = new List<Badge>();

            SimpleServerMessageBuffer simpleServerMessageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("LoadBadgesWidgetMessageComposer"));
            simpleServerMessageBuffer.AppendInteger(Count);

            foreach (Badge badge in BadgeList.Values)
            {
                simpleServerMessageBuffer.AppendInteger(1);
                simpleServerMessageBuffer.AppendString(badge.Code);

                if (badge.Slot > 0)
                    list.Add(badge);
            }

            simpleServerMessageBuffer.AppendInteger(list.Count);

            foreach (Badge current in list)
            {
                simpleServerMessageBuffer.AppendInteger(current.Slot);
                simpleServerMessageBuffer.AppendString(current.Code);
            }

            return simpleServerMessageBuffer;
        }
    }
}