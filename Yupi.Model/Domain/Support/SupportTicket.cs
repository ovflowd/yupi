using System;
using System.Collections.Generic;

namespace Yupi.Model.Domain
{
	public class SupportTicket
	{
		public virtual int Id { get; protected set; }

		public virtual UserInfo ReportedUser { get; set; }

		public virtual UserInfo Sender { get; set; }

		public virtual UserInfo Staff { get; set; }

		// TODO Use enum
		public virtual int Category { get; set; }

		public virtual string Message { get; set; }

		// TODO Should reference the chat directly!
		public virtual IList<string> ReportedChats { get; set; }

		public virtual RoomData Room { get; set; }

		public virtual int Score { get; set; }

		public virtual TicketStatus Status { get; protected set; }

		public virtual DateTime CreatedAt { get; set; }

		public virtual TicketCloseReason CloseReason { get; protected set; }

		// TODO Enum
		// type (3 or 4 for new style)
		public virtual int Type { get; set; }

		public SupportTicket ()
		{
			Status = TicketStatus.Open;
			ReportedChats = new List<string> ();
			Staff = UserInfo.None;
			CreatedAt = DateTime.Now;
		}
			
		public virtual void Pick (UserInfo moderator)
		{
			Status = TicketStatus.Picked;
			Staff = moderator;
		}
			
		public virtual void Close (TicketCloseReason reason)
		{
			Status = TicketStatus.Closed;
			CloseReason = reason;
		}

		public virtual void Release ()
		{
			Status = TicketStatus.Open;
		}
			
		public virtual void Delete ()
		{
			Status = TicketStatus.Closed;
			CloseReason = TicketCloseReason.Deleted;
		}
		/*
        /// <summary>
        ///     Serializes the specified messageBuffer.
        /// </summary>
        /// <param name="message">The messageBuffer.</param>
        /// <returns>SimpleServerMessageBuffer.</returns>
     public SimpleServerMessageBuffer Serialize(SimpleServerMessageBuffer messageBuffer)
        {
            messageBuffer.AppendInteger(TicketId);
            messageBuffer.AppendInteger(Status);
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
        }*/
	}
}