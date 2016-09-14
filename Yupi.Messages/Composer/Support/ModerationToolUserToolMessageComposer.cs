using System;
using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Support
{
    public class ModerationToolUserToolMessageComposer : Contracts.ModerationToolUserToolMessageComposer
    {
        public override void Compose(ISender session, UserInfo user)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO Refactor
                message.AppendInteger(user.Id);
                message.AppendString(user.Name);
                message.AppendString(user.Look);
                message.AppendInteger((int) (DateTime.Now - user.CreateDate).TotalHours);
                message.AppendInteger((int) (DateTime.Now - user.LastOnline).TotalHours);
                // TODO Hardcoded value
                message.AppendBool(true);
                message.AppendInteger(user.SupportTickets.Count);
                message.AppendInteger(
                    user.SupportTickets.Where(
                        x => (x.Status == TicketStatus.Closed) && (x.CloseReason == TicketCloseReason.Abusive)).Count());
                message.AppendInteger(user.Cautions.Count);
                message.AppendInteger(user.Bans.Count);

                message.AppendInteger(0);

                var tradeLock = user.TradeLocks.Last(x => !x.HasExpired());

                message.AppendString(tradeLock != null ? tradeLock.ExpiresAt.ToString() : "Not trade-locked");

                message.AppendString(user.HasPermission("view_ip") ? user.LastIp.ToString() : string.Empty);
                message.AppendInteger(user.Id);
                message.AppendInteger(0);

                // TODO Should these really be displayed here?
                message.AppendString("E-Mail:         " + user.Email);
                message.AppendString("Rank ID:        " + user.Rank);

                session.Send(message);
            }
        }
    }
}