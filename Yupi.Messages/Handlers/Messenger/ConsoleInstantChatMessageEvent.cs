using System;
using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    // TODO Rename?
    public class ConsoleInstantChatMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;
        private readonly WordfilterManager Wordfilter;

        public ConsoleInstantChatMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            Wordfilter = DependencyFactory.Resolve<WordfilterManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var toId = request.GetInteger();
            var text = request.GetString();

            if (string.IsNullOrWhiteSpace(text))
                return;

            var friend = session.Info.Relationships.FindByUser(toId);

            if (friend != null)
            {
                var message = new MessengerMessage
                {
                    From = session.Info,
                    UnfilteredText = text,
                    Text = Wordfilter.Filter(text),
                    Timestamp = DateTime.Now
                };

                var friendSession = ClientManager.GetByInfo(friend.Friend);
                friendSession?.Router.GetComposer<ConsoleChatMessageComposer>().Compose(session, message);
                message.Read = friendSession != null;

                // TODO Store for offline
                // TODO Store for chatlog
            }
        }
    }
}