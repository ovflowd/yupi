using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
    public class RoomKickUserMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage request, IRouter router)
        {
            /*
            Room room = Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId);

            if (room == null)
                return;
            // TODO Use permissions
            if (!room.CheckRights(session) && room.RoomData.WhoCanKick != 2 &&
                session.GetHabbo().Rank < Convert.ToUInt32(Yupi.GetDbConfig().DbData["ambassador.minrank"]))
                return;

            uint userId = request.GetUInt32();

            RoomUser roomUserByHabbo = room.GetRoomUserManager().GetRoomUserByHabbo(userId);

            if (roomUserByHabbo == null || roomUserByHabbo.IsBot)
                return;

            if (room.CheckRights(roomUserByHabbo.GetClient(), true) ||
                roomUserByHabbo.GetClient().UserData.Info.HasPermission("fuse_mod") ||
                roomUserByHabbo.GetClient().UserData.Info.HasPermission("fuse_no_kick")) // TODO Shouldn't we tell the user about this? (Whisper?)
                return;

            room.GetRoomUserManager().RemoveUserFromRoom(roomUserByHabbo.GetClient(), true, true);
            roomUserByHabbo.GetClient().CurrentRoomUserId = -1;
            */
            throw new NotImplementedException();
        }
    }
}