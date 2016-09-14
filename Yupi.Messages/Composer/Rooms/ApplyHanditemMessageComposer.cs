using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class ApplyHanditemMessageComposer : Contracts.ApplyHanditemMessageComposer
    {
        public override void Compose(ISender session, int virtualId, int itemId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(virtualId);
                message.AppendInteger(itemId);
                session.Send(message);
            }
        }
    }
}