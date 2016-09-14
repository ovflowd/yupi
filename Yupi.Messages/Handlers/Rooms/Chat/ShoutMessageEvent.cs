using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Chat
{
    public class ShoutMessageEvent : AbstractHandler
    {
        private readonly ChatController Chat;

        public ShoutMessageEvent()
        {
            Chat = DependencyFactory.Resolve<ChatController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            var message = request.GetString();
            var bubbleId = request.GetInteger();

            ChatBubbleStyle bubble;

            if (ChatBubbleStyle.TryFromInt32(bubbleId, out bubble)) Chat.Shout(session, message, bubble);
        }
    }
}