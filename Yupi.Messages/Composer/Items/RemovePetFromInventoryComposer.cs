using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class RemovePetFromInventoryComposer : Contracts.RemovePetFromInventoryComposer
    {
        public override void Compose(ISender session, uint petId)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(petId);
                session.Send(message);
            }
        }
    }
}