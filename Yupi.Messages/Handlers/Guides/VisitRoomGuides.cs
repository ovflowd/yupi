namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Messages.User;

    public class VisitRoomGuides : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.GuideOtherUser == null)
                return;

            router.GetComposer<RoomForwardMessageComposer>().Compose(session, session.GuideOtherUser.Room.Data.Id);
        }

        #endregion Methods
    }
}