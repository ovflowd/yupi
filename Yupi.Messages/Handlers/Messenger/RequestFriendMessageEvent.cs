namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class RequestFriendMessageEvent : AbstractHandler
    {
        #region Fields

        private ClientManager ClientManager;
        private IRepository<UserInfo> UserRepository;

        #endregion Fields

        #region Constructors

        public RequestFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string friendName = request.GetString();

            UserInfo friend = UserRepository.FindBy(x => x.Name == friendName);

            if (friend != null)
            {
                if (friend.HasFriendRequestsDisabled)
                {
                    router.GetComposer<NotAcceptingRequestsMessageComposer>().Compose(session);
                }
                else
                {
                    if (!session.Info.Relationships.HasSentRequestTo(friend))
                    {
                        var friendRequest = new FriendRequest(session.Info, friend);

                        session.Info.Relationships.SentRequests.Add(friendRequest);

                        UserRepository.Save(session.Info);

                        var friendSession = ClientManager.GetByInfo(friend);

                        if (friendSession != null)
                        {
                            friendSession.Router.GetComposer<ConsoleSendFriendRequestMessageComposer>()
                                .Compose(friendSession, friendRequest);
                        }
                    }
                }
            }
        }

        #endregion Methods
    }
}