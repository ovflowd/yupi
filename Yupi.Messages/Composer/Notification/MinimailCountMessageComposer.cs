using Yupi.Protocol;

namespace Yupi.Messages.Notification
{
    public class MinimailCountMessageComposer : Contracts.MinimailCountMessageComposer
    {
        public override void Compose(ISender session, int count)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(count);
                session.Send(message);
            }
        }
    }
}