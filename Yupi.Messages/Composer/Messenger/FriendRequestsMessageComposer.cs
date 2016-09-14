using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class FriendRequestsMessageComposer : Contracts.FriendRequestsMessageComposer
    {
        public override void Compose(ISender session, IList<FriendRequest> requests)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(requests.Count);
                message.AppendInteger(requests.Count); // TODO why the same value twice?

                foreach (var request in requests)
                {
                    message.AppendInteger(request.From.Id);
                    message.AppendString(request.From.Name);
                    message.AppendString(request.From.Look);
                }
                session.Send(message);
            }
        }
    }
}