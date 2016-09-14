using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class StartTypingMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var room = session.Room;

            if (room == null) return;

            session.Room.EachUser(
                entitySession =>
                {
                    entitySession.Router.GetComposer<TypingStatusMessageComposer>()
                        .Compose(entitySession, session.RoomEntity.Id, true);
                });
        }
    }
}