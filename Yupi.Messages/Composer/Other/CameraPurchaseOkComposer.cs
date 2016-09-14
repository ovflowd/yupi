using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class CameraPurchaseOkComposer : Contracts.CameraPurchaseOkComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                session.Send(message);
            }
        }
    }
}