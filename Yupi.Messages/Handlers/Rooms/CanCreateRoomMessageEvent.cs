namespace Yupi.Messages.Rooms
{
    using System;

    public class CanCreateRoomMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<CanCreateRoomMessageComposer>().Compose(session, session.Info);
        }

        #endregion Methods
    }
}