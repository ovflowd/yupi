using Yupi.Controller;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Chat
{
    public class UserWhisperMessageEvent : AbstractHandler
    {
        private readonly ChatController Chat;

        public UserWhisperMessageEvent()
        {
            Chat = DependencyFactory.Resolve<ChatController>();
        }

        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            var command = request.GetString();
            var bubbleId = request.GetInteger();

            ChatBubbleStyle bubble;

            if (!ChatBubbleStyle.TryFromInt32(bubbleId, out bubble)) return;

            var args = command.Split(new[] {' '}, 2);

            if (args.Length != 2) return;

            var targetUsername = args[0];
            var msg = args[1];

            var target = session.Room.GetEntity(targetUsername);

            Chat.Whisper(session, msg, bubble, target, -1);
        }
    }
}