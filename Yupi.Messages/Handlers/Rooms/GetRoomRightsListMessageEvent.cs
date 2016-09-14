namespace Yupi.Messages.Rooms
{
    using System;

    public class GetRoomRightsListMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room != null)
            {
                router.GetComposer<LoadRoomRightsListMessageComposer>().Compose(session, session.Room.Data);
            }
        }

        #endregion Methods
    }
}