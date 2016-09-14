using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class InitCryptoMessageComposer : Contracts.InitCryptoMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO What about public networks?
                message.AppendString("Yupi");
                message.AppendString("Disabled Crypto");
                session.Send(message);
            }
        }
    }
}