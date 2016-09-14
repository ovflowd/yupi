namespace Yupi.Messages.Support
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Protocol.Buffers;
    using Yupi.Util;

    public class OpenHelpToolMessageComposer : Yupi.Messages.Contracts.OpenHelpToolMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<SupportTicket> tickets)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(tickets.Count);

                foreach (SupportTicket ticket in tickets)
                {
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