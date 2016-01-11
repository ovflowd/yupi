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
    /// <summary>
    ///     Class FaceLess. This class cannot be inherited.
    /// </summary>
    internal sealed class FaceLess : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FaceLess" /> class.
        /// </summary>
        public FaceLess()
        {
            MinRank = -3;
            Description = "No Face.";
            Usage = ":faceless";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            if (!session.GetHabbo().Look.Contains("hd-"))
                return true;

            string head = session.GetHabbo().Look.Split('.').FirstOrDefault(element => element.StartsWith("hd-"));
            string color = "1";
            if (!string.IsNullOrEmpty(head))
            {
                color = head.Split('-')[2];
                session.GetHabbo().Look = session.GetHabbo().Look.Replace('.' + head, string.Empty);
            }
            session.GetHabbo().Look += ".hd-99999-" + color;

            using (IQueryAdapter dbClient = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                dbClient.SetQuery(
                    "UPDATE users SET look = @look WHERE id = " + session.GetHabbo().Id);
                dbClient.AddParameter("look", session.GetHabbo().Look);
                dbClient.RunQuery();
            }
            Room room = session.GetHabbo().CurrentRoom;
            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
            if (user == null) return true;

            ServerMessage roomUpdate = new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));
            roomUpdate.AppendInteger(user.VirtualId);
            roomUpdate.AppendString(session.GetHabbo().Look);
            roomUpdate.AppendString(session.GetHabbo().Gender.ToLower());
            roomUpdate.AppendString(session.GetHabbo().Motto);
            roomUpdate.AppendInteger(session.GetHabbo().AchievementPoints);
            room.SendMessage(roomUpdate);

            return true;
        }
    }
}