using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomGetFilterMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            var roomId = request.GetUInt32();

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(roomId);

            if (room == null || !room.CheckRights(session, true))
                return;

            router.GetComposer<RoomLoadFilterMessageComposer> ().Compose (session, room.WordFilter);
            */
            throw new NotImplementedException();
        }
    }
}