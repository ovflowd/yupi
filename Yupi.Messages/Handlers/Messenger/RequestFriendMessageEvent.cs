using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class RequestFriendMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;
        private readonly IRepository<UserInfo> UserRepository;

        public RequestFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var friendName = request.GetString();

            var friend = UserRepository.FindBy(x => x.Name == friendName);

            if (friend != null)
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
                            friendSession.Router.GetComposer<ConsoleSendFriendRequestMessageComposer>()
                                .Compose(friendSession, friendRequest);
                    }
                }
        }
    }
}