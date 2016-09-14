namespace Yupi.Messages.Rooms
{
    using System;

    using Yupi.Model.Domain;

    public class StartTypingMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            Room room = session.Room;

            if (room == null)
            {
                return;
            }

            session.Room.EachUser(
                (entitySession) =>
                {
                    entitySession.Router.GetComposer<TypingStatusMessageComposer>()
                        .Compose(entitySession, session.RoomEntity.Id, true);
                });
        }

        #endregion Methods
    }
}