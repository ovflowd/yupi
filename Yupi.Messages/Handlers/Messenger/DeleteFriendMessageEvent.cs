using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Messenger
{
    public class DeleteFriendMessageEvent : AbstractHandler
    {
        private readonly RelationshipController RelationshipController;

        public DeleteFriendMessageEvent()
        {
            RelationshipController = DependencyFactory.Resolve<RelationshipController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var count = request.GetInteger();
            for (var i = 0; i < count; i++)
            {
                var friendId = request.GetInteger();

                RelationshipController.Remove(session, friendId);
            }
        }
    }
}