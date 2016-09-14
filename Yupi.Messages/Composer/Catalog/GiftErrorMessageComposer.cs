using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class GiftErrorMessageComposer : Contracts.GiftErrorMessageComposer
    {
        public override void Compose(ISender session, string username)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString(username);
                session.Send(message);
            }
        }
    }
}