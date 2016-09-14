using Yupi.Protocol;

namespace Yupi.Messages.Other
{
    public class SecretKeyMessageComposer : Contracts.SecretKeyMessageComposer
    {
        public override void Compose(ISender session)
        {
            // TODO Public networks???

            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendString("Crypto disabled");
                message.AppendBool(false);
                session.Send(message);
            }
        }
    }
}