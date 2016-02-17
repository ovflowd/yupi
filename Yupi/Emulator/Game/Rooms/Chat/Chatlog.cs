using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Users;
using Yupi.Emulator.Messages;

namespace Yupi.Emulator.Game.Rooms.Chat
{
    /// <summary>
    ///     Class Chatlog.
    /// </summary>
    internal class Chatlog
    {
        internal bool GlobalMessage;

        /// <summary>
        ///     The is saved
        /// </summary>
        internal bool IsSaved;

        /// <summary>
        ///     The message
        /// </summary>
        internal string Message;

        /// <summary>
        ///     The timestamp
        /// </summary>
        internal DateTime TimeStamp;

        /// <summary>
        ///     The user identifier
        /// </summary>
        internal uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Chatlog" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="time">The time.</param>
        /// <param name="globalMessage"></param>
        /// <param name="fromDatabase">if set to <c>true</c> [from database].</param>
        internal Chatlog(uint user, string msg, DateTime time, bool globalMessage, bool fromDatabase = false)
        {
            UserId = user;
            Message = msg;
            TimeStamp = time;
            GlobalMessage = globalMessage;
            IsSaved = fromDatabase;
        }

        /// <summary>
        ///     Saves the specified room identifier.
        /// </summary>
        /// <param name="roomId"></param>
        internal void Save(uint roomId)
        {
            if (IsSaved)
                return;

            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery($"INSERT INTO users_chatlogs (user_id, room_id, timestamp, message) VALUES ('{UserId}', '{roomId}', '{Yupi.DateTimeToUnix(TimeStamp)}', @messageid_{UserId})");
                adapter.AddParameter("messageid_" + UserId, Message);

                adapter.RunQuery();
            }
        }

        internal void Serialize(ref ServerMessage message)
        {
            Habbo habbo = Yupi.GetHabboById(UserId);

            message.AppendInteger(Yupi.DifferenceInMilliSeconds(TimeStamp, DateTime.Now));
            message.AppendInteger(UserId);
            message.AppendString(habbo == null ? "*User not found*" : habbo.UserName);
            message.AppendString(Message);
            message.AppendBool(GlobalMessage);
        }
    }
}