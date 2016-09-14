namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Protocol.Buffers;

    public class SendRoomCampaignFurnitureMessageComposer : Yupi.Messages.Contracts.SendRoomCampaignFurnitureMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                // TODO Implement
                message.AppendInteger(0);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}