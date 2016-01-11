using System.Collections.Generic;
using System.Linq;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    internal sealed class MakePrivate : Command
    {
        public MakePrivate()
        {
            MinRank = 7;
            Description = "Haz una sala privada.";
            Usage = ":makeprivate";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;

            using (IQueryAdapter commitableQueryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                commitableQueryReactor.RunFastQuery(
                    $"UPDATE rooms_data SET roomtype = 'private' WHERE id = {room.RoomId}");

            uint roomId = session.GetHabbo().CurrentRoom.RoomId;
            List<RoomUser> users = new List<RoomUser>(session.GetHabbo().CurrentRoom.GetRoomUserManager().UserList.Values);

            Yupi.GetGame().GetRoomManager().UnloadRoom(session.GetHabbo().CurrentRoom, "Unload command");

            Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

            ServerMessage roomFwd = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(roomId);

            byte[] data = roomFwd.GetReversedBytes();

            foreach (RoomUser user in users.Where(user => user != null && user.GetClient() != null))
                user.GetClient().SendMessage(data);

            return true;
        }
    }
}