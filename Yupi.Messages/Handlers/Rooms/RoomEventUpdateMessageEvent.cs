namespace Yupi.Messages.Rooms
{
    using System;

    public class RoomEventUpdateMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            request.GetInteger(); // TODO Unused roomid?

            string name = request.GetString();
            string description = request.GetString();

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

        #endregion Methods
    }
}