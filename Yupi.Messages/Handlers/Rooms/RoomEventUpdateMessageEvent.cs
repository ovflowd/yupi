using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomEventUpdateMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            request.GetInteger(); // TODO Unused roomid?

            var name = request.GetString();
            var description = request.GetString();

            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null || !room.CheckRights(session, true) || room.RoomData.Event == null)
                return;

            room.RoomData.Event.Name = name;
            room.RoomData.Event.Description = description;

            Yupi.GetGame().GetRoomEvents().UpdateEvent(room.RoomData.Event);
            */
            throw new NotImplementedException();
        }
    }
}