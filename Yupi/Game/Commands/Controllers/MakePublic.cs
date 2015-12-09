using System.Collections.Generic;
using System.Linq;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Rooms.User;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    internal sealed class MakePublic : Command
    {
        public MakePublic()
        {
            MinRank = 7;
            Description = "Haz una sala publica.";
            Usage = ":makepublic";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            var room = session.GetHabbo().CurrentRoom;

            using (var queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery($"UPDATE rooms_data SET roomtype = 'public' WHERE id = {room.RoomId}");

            var roomId = session.GetHabbo().CurrentRoom.RoomId;
            var users = new List<RoomUser>(session.GetHabbo().CurrentRoom.GetRoomUserManager().UserList.Values);

            Yupi.GetGame().GetRoomManager().UnloadRoom(session.GetHabbo().CurrentRoom, "Unload command");

            Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

            var roomFwd = new ServerMessage(LibraryParser.OutgoingRequest("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(roomId);

            var data = roomFwd.GetReversedBytes();

            foreach (var user in users.Where(user => user != null && user.GetClient() != null))
                user.GetClient().SendMessage(data);

            return true;
        }
    }
}