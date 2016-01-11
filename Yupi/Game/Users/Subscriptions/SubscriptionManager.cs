using System;
using System.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Users.Data.Models;

namespace Yupi.Game.Users.Subscriptions
{
    /// <summary>
    ///     Class SubscriptionManager.
    /// </summary>
    internal class SubscriptionManager
    {
        /// <summary>
        ///     The _user identifier
        /// </summary>
        private readonly uint _userId;

        /// <summary>
        ///     The _subscription
        /// </summary>
        private Subscription _subscription;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubscriptionManager" /> class.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userData">The user data.</param>
        internal SubscriptionManager(uint userId, UserData userData)
        {
            _userId = userId;
            _subscription = userData.Subscriptions;
        }

        /// <summary>
        ///     Gets a value indicating whether this instance has subscription.
        /// </summary>
        /// <value><c>true</c> if this instance has subscription; otherwise, <c>false</c>.</value>
        internal bool HasSubscription => _subscription != null && _subscription.IsValid;

        /// <summary>
        ///     Gets the subscription.
        /// </summary>
        /// <returns>Subscription.</returns>
        internal Subscription GetSubscription()
        {
            return _subscription;
        }

        /// <summary>
        ///     Adds the subscription.
        /// </summary>
        /// <param name="dayLength">Length of the day.</param>
        internal void AddSubscription(double dayLength)
        {
            int num = (int) Math.Round(dayLength);

            GameClient clientByUserId = Yupi.GetGame().GetClientManager().GetClientByUserId(_userId);
            DateTime target;
            int num2;
            int num3;

            if (_subscription != null)
            {
                target = Yupi.UnixToDateTime(_subscription.ExpireTime).AddDays(num);
                num2 = _subscription.ActivateTime;
                num3 = _subscription.LastGiftTime;
            }
            else
            {
                target = DateTime.Now.AddDays(num);
                num2 = Yupi.GetUnixTimeStamp();
                num3 = Yupi.GetUnixTimeStamp();
            }

            int num4 = Yupi.DateTimeToUnix(target);
            _subscription = new Subscription(2, num2, num4, num3);

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(string.Concat("REPLACE INTO users_subscriptions VALUES (", _userId,
                    ", 2, ",
                    num2, ", ", num4, ", ", num3, ");"));

            clientByUserId.GetHabbo().SerializeClub();
            Yupi.GetGame().GetAchievementManager().TryProgressHabboClubAchievements(clientByUserId);
        }

        /// <summary>
        ///     Reloads the subscription.
        /// </summary>
        internal void ReloadSubscription()
        {
            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                commitableQueryReactor.SetQuery(
                    "SELECT * FROM users_subscriptions WHERE user_id=@id AND timestamp_expire > UNIX_TIMESTAMP() ORDER BY subscription_id DESC LIMIT 1");
                commitableQueryReactor.AddParameter("id", _userId);

                DataRow row = commitableQueryReactor.GetRow();

                _subscription = row == null
                    ? null
                    : new Subscription((int) row[1], (int) row[2], (int) row[3], (int) row[4]);
            }
        }
    }
}