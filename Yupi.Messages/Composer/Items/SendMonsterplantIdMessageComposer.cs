using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class SendMonsterplantIdMessageComposer : Contracts.SendMonsterplantIdMessageComposer
    {
        public override void Compose(ISender session, uint entityId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                session.Send(message);
            }
        }
    }
}