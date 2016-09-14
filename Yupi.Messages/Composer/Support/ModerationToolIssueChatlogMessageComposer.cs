using System;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Support
{
    public class ModerationToolIssueChatlogMessageComposer : Contracts.ModerationToolIssueChatlogMessageComposer
    {
        // TODO Refactor
        public override void Compose(ISender session, SupportTicket ticket)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(ticket.Id);
                message.AppendInteger(ticket.Sender.Id);
                message.AppendInteger(ticket.ReportedUser.Id);
                message.AppendInteger(ticket.Room.Id);

                // TODO Hardcoded message
                message.AppendByte(1);
                message.AppendShort(2);
                message.AppendString("roomName");
                message.AppendByte(2);
                message.AppendString(ticket.Room.Name);
                message.AppendString("roomId");
                message.AppendByte(1);
                message.AppendInteger(ticket.Room.Id);

                var count = Math.Min(ticket.Room.Chatlog.Count, 60);
                message.AppendShort((short) count);

                for (var i = 1; i <= count; ++i)
                {
                    var entry = ticket.Room.Chatlog[ticket.Room.Chatlog.Count - i];
                    message.AppendInteger((int) (DateTime.Now - entry.Timestamp).TotalMilliseconds);
                    message.AppendInteger(entry.User.Id);
                    message.AppendString(entry.User.Name);
                    message.AppendString(entry.Message);
                    message.AppendBool(!entry.Whisper);
                }

                session.Send(message);
            }
        }
    }
}