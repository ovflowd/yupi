using System.Collections.Generic;
using System.Linq;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;



namespace Yupi.Emulator.Game.Commands.Controllers
{
     public sealed class MakePrivate : Command
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

            using (IQueryAdapter queryReactor = Yupi.GetDatabaseManager().GetQueryReactor())
                queryReactor.RunFastQuery(
                    $"UPDATE rooms_data SET roomtype = 'private' WHERE id = {room.RoomId}");

            uint roomId = session.GetHabbo().CurrentRoom.RoomId;
            List<RoomUser> users = new List<RoomUser>(session.GetHabbo().CurrentRoom.GetRoomUserManager().UserList.Values);

            Yupi.GetGame().GetRoomManager().UnloadRoom(session.GetHabbo().CurrentRoom, "Unload command");
            Yupi.GetGame().GetRoomManager().LoadRoom(roomId);

            foreach (RoomUser user in users.Where(user => user != null && user.GetClient() != null))
				user.GetClient().CurrentRoom.Router.GetComposer<RoomForwardMessageComposer> ().Compose (user.GetClient(), roomId);

            return true;
        }
    }
}