using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomUnbanUserMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var userId = request.GetUInt32();
            var roomId = request.GetUInt32();

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null)
                return;

            room.Unban(userId);

            router.GetComposer<RoomUnbanUserMessageComposer> ().Compose (session, roomId, userId);
            */
            throw new NotImplementedException();
        }
    }
}