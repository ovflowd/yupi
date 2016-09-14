namespace Yupi.Messages.Messenger
{
    using System;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class ConsoleSendFriendRequestMessageComposer : Yupi.Messages.Contracts.ConsoleSendFriendRequestMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, FriendRequest request)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(request.From.Id);
                message.AppendString(request.From.Name);
                message.AppendString(request.From.Look);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}