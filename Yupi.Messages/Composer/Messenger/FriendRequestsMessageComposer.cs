namespace Yupi.Messages.Messenger
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol.Buffers;

    public class FriendRequestsMessageComposer : Yupi.Messages.Contracts.FriendRequestsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, IList<FriendRequest> requests)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(requests.Count);
                message.AppendInteger(requests.Count); // TODO why the same value twice?

                foreach (FriendRequest request in requests)
                {
                    message.AppendInteger(request.From.Id);
                    message.AppendString(request.From.Name);
                    message.AppendString(request.From.Look);
                }
                session.Send(message);
            }
        }

        #endregion Methods
    }
}