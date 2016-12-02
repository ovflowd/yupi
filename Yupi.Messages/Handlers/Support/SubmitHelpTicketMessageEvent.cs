// ---------------------------------------------------------------------------------
// <copyright file="SubmitHelpTicketMessageEvent.cs" company="https://github.com/sant0ro/Yupi">
//   Copyright (c) 2016 Claudio Santoro, TheDoctor
// </copyright>
// <license>
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in
//   all copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//   THE SOFTWARE.
// </license>
// ---------------------------------------------------------------------------------
namespace Yupi.Messages.Support
{
    using System;
    using System.Linq;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class SubmitHelpTicketMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<RoomData> RoomRepository;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public SubmitHelpTicketMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            RoomRepository = DependencyFactory.Resolve<IRepository<RoomData>>();
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string content = message.GetString();
            int category = message.GetInteger();
            int reportedUserId = message.GetInteger();
            int roomId = message.GetInteger();

            int messageCount = message.GetInteger();

            string[] chats = new string[messageCount];

            for (int i = 0; i < messageCount; ++i)
            {
                // TODO Unused?!
                message.GetInteger();
                // TODO Validate!
                chats[i] = message.GetString();
            }

            var pendingTickets = session.Info.SupportTickets.Where(x => x.Status != TicketStatus.Closed);
            var lastAbusive =
                session.Info.SupportTickets.OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault(x => x.Status == TicketStatus.Closed && x.CloseReason == TicketCloseReason.Abusive);

            if (pendingTickets.Count() != 0)
            {
                router.GetComposer<TicketUserAlertComposer>()
                    .Compose(session, TicketUserAlertComposer.Status.HAS_PENDING, pendingTickets.First());
                // TODO Magic constant!
            }
            else if (lastAbusive != null && (DateTime.Now - lastAbusive.CreatedAt).TotalMinutes < 10)
            {
                router.GetComposer<TicketUserAlertComposer>()
                    .Compose(session, TicketUserAlertComposer.Status.PREVIOUS_ABUSIVE);
            }
            else
            {
                router.GetComposer<TicketUserAlertComposer>().Compose(session, TicketUserAlertComposer.Status.OK);

                RoomData room = RoomRepository.Find(roomId);
                UserInfo reportedUser = UserRepository.Find(reportedUserId);
                throw new NotImplementedException ();
                /*
                SupportTicket newTicket = new SupportTicket()
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

                foreach (Habbo staff in ClientManager.GetByPermission("handle_cfh"))
                {
                    // TODO Create a controller for this...
                    staff.Router.GetComposer<ModerationToolIssueMessageComposer>().Compose(staff, newTicket);
                }*/
            }
        }

        #endregion Methods
    }
}