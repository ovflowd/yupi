using Yupi.Protocol;

namespace Yupi.Messages.Wired
{
    public class WiredRewardAlertMessageComposer : Contracts.WiredRewardAlertMessageComposer
    {
        public override void Compose(ISender session, int status)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(status); // TODO Use enum
                session.Send(message);
            }
        }
    }
}