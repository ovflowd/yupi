using Yupi.Protocol;

namespace Yupi.Messages.Items
{
    public class BuildersClubUpdateFurniCountMessageComposer : Contracts.BuildersClubUpdateFurniCountMessageComposer
    {
        public override void Compose(ISender session, int itemsUsed)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(itemsUsed);
                session.Send(message);
            }
        }
    }
}