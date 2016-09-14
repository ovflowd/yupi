namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomOnLoadMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<SendRoomCampaignFurnitureMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}