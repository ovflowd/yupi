namespace Yupi.Messages.Guides
{
    using System;

    using Yupi.Model.Domain;

    public class GuideInviteToRoom : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            Room room = session.Room;

            if (session.Room == null || session.GuideOtherUser == null)
            {
                return;
            }

            router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer>()
                .Compose(session.GuideOtherUser, room.Data.Id, room.Data.Name);

            // TODO Is this really required
            router.GetComposer<OnGuideSessionInvitedToGuideRoomMessageComposer>()
                .Compose(session, room.Data.Id, room.Data.Name);
        }

        #endregion Methods
    }
}