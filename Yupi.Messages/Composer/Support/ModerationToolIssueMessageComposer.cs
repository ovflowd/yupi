using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Support
{
    public class ModerationToolIssueMessageComposer : Yupi.Messages.Contracts.ModerationToolIssueMessageComposer
    {
        public override void Compose(Yupi.Protocol.ISender session, SupportTicket ticket)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(ticket.Id);
                message.AppendInteger((int) ticket.Status);
                message.AppendInteger(ticket.Type);
                message.AppendInteger(ticket.Category);
                message.AppendInteger((int) (DateTime.Now - ticket.CreatedAt).TotalMilliseconds);
                message.AppendInteger(ticket.Score);
                message.AppendInteger(1); // TODO magic constant
                message.AppendInteger(ticket.Sender.Id);
                message.AppendString(ticket.Sender.Name);
                message.AppendInteger(ticket.ReportedUser.Id);
                message.AppendString(ticket.ReportedUser.Name);
                message.AppendInteger(ticket.Staff.Id);
                message.AppendString(ticket.Staff.Name);
                message.AppendString(ticket.Message);
                message.AppendInteger(0);

                message.AppendInteger(ticket.ReportedChats.Count);

                foreach (string str in ticket.ReportedChats)
                {
                    message.AppendString(str);
                    message.AppendInteger(-1);
                    message.AppendInteger(-1);
                }

                session.Send(message);
            }
        }
    }
}