using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Messenger
{
    public class ConsoleSendFriendRequestMessageComposer : Contracts.ConsoleSendFriendRequestMessageComposer
    {
        public override void Compose(ISender session, FriendRequest request)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(request.From.Id);
                message.AppendString(request.From.Name);
                message.AppendString(request.From.Look);
                session.Send(message);
            }
        }
    }
}