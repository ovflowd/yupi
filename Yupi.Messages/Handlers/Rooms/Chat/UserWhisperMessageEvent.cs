namespace Yupi.Messages.Chat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Yupi.Controller;
    using Yupi.Model;
    using Yupi.Model.Domain;

    public class UserWhisperMessageEvent : AbstractHandler
    {
        #region Fields

        private ChatController Chat;

        #endregion Fields

        #region Constructors

        public UserWhisperMessageEvent()
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

            string command = request.GetString();
            int bubbleId = request.GetInteger();

            ChatBubbleStyle bubble;

            if (!ChatBubbleStyle.TryFromInt32(bubbleId, out bubble))
            {
                return;
            }

            string[] args = command.Split(new char[] {' '}, 2);

            if (args.Length != 2)
            {
                return;
            }

            string targetUsername = args[0];
            string msg = args[1];

            RoomEntity target = session.Room.GetEntity(targetUsername);

            Chat.Whisper(session, msg, bubble, target, -1);
        }

        #endregion Methods
    }
}