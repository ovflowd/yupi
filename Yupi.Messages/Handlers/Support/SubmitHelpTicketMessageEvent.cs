using System;
using System.Linq;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Support
{
    public class SubmitHelpTicketMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<RoomData> RoomRepository;
        private readonly IRepository<UserInfo> UserRepository;

        public SubmitHelpTicketMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var content = message.GetString();
            var category = message.GetInteger();
            var reportedUserId = message.GetInteger();
            var roomId = message.GetInteger();

            var messageCount = message.GetInteger();

            var chats = new string[messageCount];

            for (var i = 0; i < messageCount; ++i)
            {
                // TODO Unused?!
                message.GetInteger();
                // TODO Validate!
                chats[i] = message.GetString();
            }

            var pendingTickets = session.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);
            var lastAbusive =
                session.Info.SupportTickets.OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault(
                        x => (x.Status == TicketStatus.Closed) && (x.CloseReason == TicketCloseReason.Abusive));

            if (pendingTickets.Count() != 0)
            {
                router.GetComposer<TicketUserAlertComposer>()
                    .Compose(session, Contracts.TicketUserAlertComposer.Status.HAS_PENDING, pendingTickets.First());
                // TODO Magic constant!
            }
            else if ((lastAbusive != null) && ((DateTime.Now - lastAbusive.CreatedAt).TotalMinutes < 10))
            {
                router.GetComposer<TicketUserAlertComposer>()
                    .Compose(session, Contracts.TicketUserAlertComposer.Status.PREVIOUS_ABUSIVE);
            }
            else
            {
                router.GetComposer<TicketUserAlertComposer>()
                    .Compose(session, Contracts.TicketUserAlertComposer.Status.OK);

                var room = RoomRepository.FindBy(roomId);
                var reportedUser = UserRepository.FindBy(reportedUserId);

                var newTicket = new SupportTicket
                {
                    Category = category,
                    Message = content,
                    ReportedChats = chats,
                    ReportedUser = reportedUser,
                    Room = room,
                    Type = 7 // TODO Hardcoded value
                };

                session.Info.SupportTickets.Add(newTicket);
                UserRepository.Save(session.Info);

                foreach (var staff in ClientManager.GetByPermission("handle_cfh"))
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, newTicket);
            }
        }
    }
}