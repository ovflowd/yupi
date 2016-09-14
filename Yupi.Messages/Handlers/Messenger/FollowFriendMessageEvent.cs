using Yupi.Controller;
using Yupi.Messages.User;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class FollowFriendMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;

        public FollowFriendMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        // TODO Refactor
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var userId = request.GetInteger();

            var relationship = session.Info.Relationships.FindByUser(userId);

            if (relationship == null)
            {
                router.GetComposer<FollowFriendErrorMessageComposer>().Compose(session, 0);
            }
            else
            {
                var friendSession = ClientManager.GetByUserId(userId);

                if ((friendSession == null) || (friendSession.Room == null))
                    router.GetComposer<FollowFriendErrorMessageComposer>().Compose(session, 2);
                else router.GetComposer<RoomForwardMessageComposer>().Compose(session, friendSession.Room.Data.Id);
            }
        }
    }
}