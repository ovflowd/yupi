namespace Yupi.Messages.Support
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;
    using Yupi.Util;

    public class TicketUserAlertComposer : Yupi.Messages.Contracts.TicketUserAlertComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, Status status, SupportTicket ticket = null)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger((int) status);

                if (status == Status.HAS_PENDING)
                {
                    Debug.Assert(ticket != null);

                    message.AppendString(ticket.Id.ToString());
                    message.AppendString(ticket.CreatedAt.ToUnix().ToString());
                    message.AppendString(ticket.Message);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}