using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomSettingsMuteAllMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Room currentRoom = session.GetHabbo().CurrentRoom;

            if (currentRoom == null || !currentRoom.CheckRights(session, true))
                return;

            currentRoom.RoomMuted = !currentRoom.RoomMuted;

            router.GetComposer<RoomMuteStatusMessageComposer> ().Compose (currentRoom, currentRoom.RoomMuted);
            */
            throw new NotImplementedException();
        }
    }
}