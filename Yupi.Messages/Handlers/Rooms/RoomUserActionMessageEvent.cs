using Yupi.Messages.User;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomUserActionMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            var actionId = request.GetInteger();

            UserAction action;

            if (!UserAction.TryParse(actionId, out action)) return;

            if (action == UserAction.Idle)
            {
                session.RoomEntity.Sleep();
            }
            else
            {
                session.RoomEntity.Wake();

                session.RoomEntity.Room.EachUser(
                    roomSession =>
                    {
                        roomSession.Router.GetComposer<RoomUserActionMessageComposer>()
                            .Compose(roomSession, session.RoomEntity.Id, action);
                    });
            }
        }
    }
}