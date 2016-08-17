using System;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Users;


namespace Yupi.Emulator.Game.Rooms.Chat
{
    /// <summary>
    ///     Class Chatlog.
    /// </summary>
     public class Chatlog
    {
     public bool GlobalMessage;

        /// <summary>
        ///     The is saved
        /// </summary>
     public bool IsSaved;

        /// <summary>
        ///     The messageBuffer
        /// </summary>
     public string Message;

        /// <summary>
        ///     The timestamp
        /// </summary>
     public DateTime TimeStamp;

        /// <summary>
        ///     The user identifier
        /// </summary>
     public uint UserId;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Chatlog" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="time">The time.</param>
        /// <param name="globalMessage"></param>
        /// <param name="fromDatabase">if set to <c>true</c> [from database].</param>
     public Chatlog(uint user, string msg, DateTime time, bool globalMessage, bool fromDatabase = false)
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
     public void Save(uint roomId)
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
		/*
     public void Serialize(ref SimpleServerMessageBuffer messageBuffer)
        {
            Habbo habbo = Yupi.GetHabboById(UserId);

            messageBuffer.AppendInteger(Yupi.DifferenceInMilliSeconds(TimeStamp, DateTime.Now));
            messageBuffer.AppendInteger(UserId);
            messageBuffer.AppendString(habbo == null ? "*User not found*" : habbo.UserName);
            messageBuffer.AppendString(Message);
            messageBuffer.AppendBool(GlobalMessage);
        }*/
    }
}