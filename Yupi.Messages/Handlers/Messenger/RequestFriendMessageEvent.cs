using System;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Model;
using Yupi.Controller;

namespace Yupi.Messages.Messenger
{
    public class RequestFriendMessageEvent : AbstractHandler
    {
        private IRepository<UserInfo> UserRepository;
        private ClientManager ClientManager;

        public RequestFriendMessageEvent()
        {
            UserRepository = DependencyFactory.Resolve<IRepository<UserInfo>>();
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

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
    }
}