using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class UserSignMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            if (session.RoomEntity == null)
                return;

            session.RoomEntity.Wake();

            var value = request.GetInteger();

            Sign sign;

            if (Sign.TryFromInt32(value, out sign)) session.RoomEntity.Status.Sign(sign);
        }
    }
}