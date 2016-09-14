using Yupi.Protocol;

namespace Yupi.Messages.Pets
{
    public class RespectPetMessageComposer : Contracts.RespectPetMessageComposer
    {
        public override void Compose(ISender session, int entityId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(entityId);
                message.AppendBool(true);
                session.Send(message);
            }
        }
    }
}