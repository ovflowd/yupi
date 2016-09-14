using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class UserDanceMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            session.RoomEntity.Wake();

            var danceId = request.GetInteger();

            Dance dance;

            if (Dance.TryFromInt32(danceId, out dance)) session.RoomEntity.SetDance(dance);
        }
    }
}