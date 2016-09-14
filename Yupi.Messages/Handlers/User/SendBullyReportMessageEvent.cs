namespace Yupi.Messages.Handlers.User
{
    using System;
    using System.Collections.Generic;

    using Yupi.Controller;
    using Yupi.Messages.Support;
    using Yupi.Messages.User;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;
    using Yupi.Net;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class SendBullyReportMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int reportedId = message.GetInteger();

            UserInfo reportedUser = DependencyFactory.Resolve<IRepository<UserInfo>>().FindBy(reportedId);

            SupportTicket ticket = new SupportTicket()
            {
                Category = 104,
                Type = 9,
                ReportedUser = reportedUser
            };

            DependencyFactory.Resolve<ModerationTool>().Tickets.Add(ticket);

            router.GetComposer<BullyReportSentMessageComposer>().Compose(session);
            router.GetComposer<ModerationToolIssueMessageComposer>()
                .Compose(DependencyFactory.Resolve<StaffSender>(), ticket);
        }

        #endregion Methods
    }
}