using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class ConsoleInviteFriendsMessageEvent : AbstractHandler
    {
        private readonly ClientManager ClientManager;

        public ConsoleInviteFriendsMessageEvent()
        {
            ClientManager = DependencyFactory.Resolve<ClientManager>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var count = request.GetInteger();

            var users = new int[count];

            for (var i = 0; i < count; i++) users[i] = request.GetInteger();

            var content = request.GetString();

            foreach (var userId in users)
            {
                var relationship = session.Info.Relationships.FindByUser(userId);
                if (relationship == null) continue;

                var friendSession = ClientManager.GetByInfo(relationship.Friend);

                friendSession?.Router.GetComposer<ConsoleInvitationMessageComposer>()
                    .Compose(friendSession, session.Info.Id, content);
            }
        }
    }
}