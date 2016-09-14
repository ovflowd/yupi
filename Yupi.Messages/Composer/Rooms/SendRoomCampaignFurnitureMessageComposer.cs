using Yupi.Protocol;

namespace Yupi.Messages.Rooms
{
    public class SendRoomCampaignFurnitureMessageComposer : Contracts.SendRoomCampaignFurnitureMessageComposer
    {
        public override void Compose(ISender session)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                // TODO Implement
                message.AppendInteger(0);
                session.Send(message);
            }
        }
    }
}