namespace Yupi.Messages.Chat
{
    using System;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class ChatMessageEvent : AbstractHandler
    {
        #region Fields

        private ChatController Chat;

        #endregion Fields

        #region Constructors

        public ChatMessageEvent()
        {
            Chat = DependencyFactory.Resolve<ChatController>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            string message = request.GetString();
            int bubbleId = request.GetInteger();
            int count = request.GetInteger();

            ChatBubbleStyle bubble;

            if (ChatBubbleStyle.TryFromInt32(bubbleId, out bubble))
            {
                Chat.Chat(session, message, bubble);
            }
        }

        #endregion Methods
    }
}