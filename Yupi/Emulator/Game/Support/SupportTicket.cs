using System;
using System.Collections.Generic;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Support
{
    /// <summary>
    ///     Class SupportTicket.
    /// </summary>
     public class SupportTicket
    {
        /// <summary>
        ///     The _reported name
        /// </summary>
        private readonly string _reportedName;

        /// <summary>
        ///     The _sender name
        /// </summary>
        private readonly string _senderName;

        /// <summary>
        ///     The _mod name
        /// </summary>
        private string _modName;

        /// <summary>
        ///     The category
        /// </summary>
     public int Category;

        /// <summary>
        ///     The messageBuffer
        /// </summary>
     public string Message;

        /// <summary>
        ///     The moderator identifier
        /// </summary>
     public uint ModeratorId;

        /// <summary>
        ///     The reported chats
        /// </summary>
     public List<string> ReportedChats;

        /// <summary>
        ///     The reported identifier
        /// </summary>
     public uint ReportedId;

        /// <summary>
        ///     The room identifier
        /// </summary>
     public uint RoomId;

        /// <summary>
        ///     The room name
        /// </summary>
     public string RoomName;

        /// <summary>
        ///     The score
        /// </summary>
     public int Score;

        /// <summary>
        ///     The sender identifier
        /// </summary>
     public uint SenderId;

        /// <summary>
        ///     The status
        /// </summary>
     public TicketStatus Status;

        /// <summary>
        ///     The timestamp
        /// </summary>
     public double Timestamp;

        /// <summary>
        ///     The type
        /// </summary>
     public int Type;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SupportTicket" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="score">The score.</param>
        /// <param name="category">The category.</param>
        /// <param name="type">The type.</param>
        /// <param name="senderId">The sender identifier.</param>
        /// <param name="reportedId">The reported identifier.</param>
        /// <param name="message">The messageBuffer.</param>
        /// <param name="roomId">The room identifier.</param>
        /// <param name="roomName">Name of the room.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="reportedChats">The reported chats.</param>
     public SupportTicket(uint id, int score, int category, int type, uint senderId, uint reportedId,
            string message, uint roomId, string roomName, double timestamp, List<string> reportedChats)
        {
            TicketId = id;
            Score = score;
            Category = category;
            Type = type;
            Status = TicketStatus.Open;
            SenderId = senderId;
            ReportedId = reportedId;
            ModeratorId = 0u;
            Message = message;
            RoomId = roomId;
            RoomName = roomName;
            Timestamp = timestamp;
            _senderName = Yupi.GetGame().GetClientManager().GetUserNameByUserId(senderId);
            _reportedName = Yupi.GetGame().GetClientManager().GetUserNameByUserId(reportedId);
            _modName = Yupi.GetGame().GetClientManager().GetUserNameByUserId(ModeratorId);
            ReportedChats = reportedChats;
        }

        /// <summary>
        ///     Gets the tab identifier.
        /// </summary>
        /// <value>The tab identifier.</value>
     public int TabId
        {
            get
            {
                if (Status == TicketStatus.Open)
                    return 1;
                if (Status == TicketStatus.Picked)
                    return 2;
                if ((Status == TicketStatus.Abusive) || (Status == TicketStatus.Invalid) ||
                    (Status == TicketStatus.Resolved))
                    return 0;
                if (Status == TicketStatus.Deleted)
                    return 0;
                return 0;
            }
        }

        /// <summary>
        ///     Gets the ticket identifier.
        /// </summary>
        /// <value>The ticket identifier.</value>
     public uint TicketId { get; }

        /// <summary>
        ///     Picks the specified p moderator identifier.
        /// </summary>
        /// <param name="pModeratorId">The p moderator identifier.</param>
        /// <param name="updateInDb">if set to <c>true</c> [update in database].</param>
     public void Pick(uint pModeratorId, bool updateInDb)
        {
            Status = TicketStatus.Picked;
            ModeratorId = pModeratorId;
            _modName = Yupi.GetHabboById(pModeratorId).UserName;

            if (!updateInDb)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    string.Concat("UPDATE moderation_tickets SET status = 'picked', moderator_id = ", pModeratorId,
                        ", timestamp = '", Yupi.GetUnixTimeStamp(), "' WHERE id = ", TicketId));
        }

        /// <summary>
        ///     Closes the specified new status.
        /// </summary>
        /// <param name="newStatus">The new status.</param>
        /// <param name="updateInDb">if set to <c>true</c> [update in database].</param>
     public void Close(TicketStatus newStatus, bool updateInDb)
        {
            Status = newStatus;

            if (!updateInDb)
                return;

            string statusCode = "resolved";

            switch (newStatus)
            {
                case TicketStatus.Abusive:
                    statusCode = "abusive";
                    break;

                case TicketStatus.Invalid:
                    statusCode = "invalid";
                    break;

                case TicketStatus.Resolved:
                    statusCode = "resolved";
                    break;

                case TicketStatus.Open:
                    break;

                case TicketStatus.Picked:
                    break;

                case TicketStatus.Deleted:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(newStatus), newStatus, null);
            }

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE moderation_tickets SET status = '{statusCode}' WHERE id = {TicketId}");
        }

        /// <summary>
        ///     Releases the specified update in database.
        /// </summary>
        /// <param name="updateInDb">if set to <c>true</c> [update in database].</param>
     public void Release(bool updateInDb)
        {
            Status = TicketStatus.Open;

            if (!updateInDb)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE moderation_tickets SET status = 'open' WHERE id = {TicketId}");
        }

        /// <summary>
        ///     Deletes the specified update in database.
        /// </summary>
        /// <param name="updateInDb">if set to <c>true</c> [update in database].</param>
     public void Delete(bool updateInDb)
        {
            Status = TicketStatus.Deleted;

            if (!updateInDb)
                return;

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE moderation_tickets SET status = 'deleted' WHERE id = {TicketId}");
        }

        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(TicketId);
            messageBuffer.AppendInteger(TabId);
            messageBuffer.AppendInteger(Type); // type (3 or 4 for new style)
            messageBuffer.AppendInteger(Category);
            messageBuffer.AppendInteger((Yupi.GetUnixTimeStamp() - (int) Timestamp)*1000);
            messageBuffer.AppendInteger(Score);
            messageBuffer.AppendInteger(1);
            messageBuffer.AppendInteger(SenderId);
            messageBuffer.AppendString(_senderName);
            messageBuffer.AppendInteger(ReportedId);
            messageBuffer.AppendString(_reportedName);
            messageBuffer.AppendInteger(Status == TicketStatus.Picked ? ModeratorId : 0);
            messageBuffer.AppendString(_modName);
            messageBuffer.AppendString(Message);
            messageBuffer.AppendInteger(0);

            messageBuffer.AppendInteger(ReportedChats.Count);

            foreach (string str in ReportedChats)
            {
                messageBuffer.AppendString(str);
                messageBuffer.AppendInteger(-1);
                messageBuffer.AppendInteger(-1);
            }

            return messageBuffer;
        }
    }
}