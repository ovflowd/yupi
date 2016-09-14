namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    // TODO Rename?
    public class ConsoleInstantChatMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;
        private WordfilterManager Wordfilter;

        #endregion Fields

        #region Constructors

        public ConsoleInstantChatMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
            Wordfilter = DependencyFactory.Resolve<WordfilterManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            int toId = request.GetInteger();
            string text = request.GetString();

            if (string.IsNullOrWhiteSpace(text))
                return;

            Relationship friend = session.Info.Relationships.FindByUser(toId);

            if (friend != null)
            {
                MessengerMessage message = new MessengerMessage()
                {
                    From = session.Info,
                    UnfilteredText = text,
                    Text = Wordfilter.Filter(text),
                    Timestamp = DateTime.Now,
                };

                var friendSession = ClientManager.GetByInfo(friend.Friend);
                friendSession?.Router.GetComposer<ConsoleChatMessageComposer>().Compose(session, message);
                message.Read = friendSession != null;

                // TODO Store for offline
                // TODO Store for chatlog
            }
        }

        #endregion Methods
    }
}