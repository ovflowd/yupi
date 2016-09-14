using Yupi.Protocol;

namespace Yupi.Messages.Catalog
{
    public class ReloadEcotronMessageComposer : Contracts.ReloadEcotronMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO Hardcoded message
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(1);
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}