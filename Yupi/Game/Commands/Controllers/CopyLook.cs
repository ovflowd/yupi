using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class CopyLook. This class cannot be inherited.
    /// </summary>
    internal sealed class CopyLook : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CopyLook" /> class.
        /// </summary>
        public CopyLook()
        {
            MinRank = -3;
            Description = "Copys a look of another user.";
            Usage = ":copy [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            var room = session.GetHabbo().CurrentRoom;

            var user = room.GetRoomUserManager().GetRoomUserByHabbo(pms[0]);
            if (user == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }

            var gender = user.GetClient().GetHabbo().Gender;
            var look = user.GetClient().GetHabbo().Look;
            session.GetHabbo().Gender = gender;
            session.GetHabbo().Look = look;
            using (var adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "UPDATE users SET gender = @gender, look = @look WHERE id = " + session.GetHabbo().Id);
                adapter.AddParameter("gender", gender);
                adapter.AddParameter("look", look);
                adapter.RunQuery();
            }

            var myUser = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().UserName);
            if (myUser == null) return true;

            var message = new ServerMessage(LibraryParser.OutgoingRequest("UpdateUserDataMessageComposer"));
            message.AppendInteger(myUser.VirtualId);
            message.AppendString(session.GetHabbo().Look);
            message.AppendString(session.GetHabbo().Gender.ToLower());
            message.AppendString(session.GetHabbo().Motto);
            message.AppendInteger(session.GetHabbo().AchievementPoints);
            room.SendMessage(message.GetReversedBytes());

            return true;
        }
    }
}