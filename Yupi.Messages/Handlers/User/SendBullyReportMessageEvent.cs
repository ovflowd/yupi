using Yupi.Controller;
using Yupi.Messages.Support;
using Yupi.Messages.User;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Handlers.User
{
    public class SendBullyReportMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var reportedId = message.GetInteger();

            var reportedUser = DependencyFactory.Resolve<IRepository<UserInfo>>().FindBy(reportedId);

            var ticket = new SupportTicket
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
    }
}