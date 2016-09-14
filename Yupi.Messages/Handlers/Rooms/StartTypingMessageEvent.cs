using System;
using Yupi.Model.Domain;


namespace Yupi.Messages.Rooms
{
    public class StartTypingMessageEvent : AbstractHandler
    {
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
    }
}