using System;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Users;
using Yupi.Messages;

namespace Yupi.Game.Rooms.Chat
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
        /// <param name="adapter"></param>
        /// <param name="roomId"></param>
        internal void Save(IQueryAdapter adapter, uint roomId)
        {
            if (IsSaved)
                return;

            adapter.SetQuery("INSERT INTO users_chatlogs (user_id, room_id, timestamp, message) VALUES (@user, @room, @time, @message)");
            adapter.AddParameter("user", UserId);
            adapter.AddParameter("room", roomId);
            adapter.AddParameter("time", Yupi.DateTimeToUnix(TimeStamp));
            adapter.AddParameter("message", Message);

            adapter.RunQuery();
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