using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;
using Yupi.Emulator.Game.Rooms.User;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;
using Yupi.Emulator.Messages.Parsers;

namespace Yupi.Emulator.Game.Commands.Controllers
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
            Room room = session.GetHabbo().CurrentRoom;

            RoomUser user = room.GetRoomUserManager().GetRoomUserByHabbo(pms[0]);
            if (user == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }

            string gender = user.GetClient().GetHabbo().Gender;
            string look = user.GetClient().GetHabbo().Look;
            session.GetHabbo().Gender = gender;
            session.GetHabbo().Look = look;
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                adapter.SetQuery(
                    "UPDATE users SET gender = @gender, look = @look WHERE id = " + session.GetHabbo().Id);
                adapter.AddParameter("gender", gender);
                adapter.AddParameter("look", look);
                adapter.RunQuery();
            }

            RoomUser myUser = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().UserName);
            if (myUser == null) return true;

            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingRequest("UpdateUserDataMessageComposer"));
            messageBuffer.AppendInteger(myUser.VirtualId);
            messageBuffer.AppendString(session.GetHabbo().Look);
            messageBuffer.AppendString(session.GetHabbo().Gender.ToLower());
            messageBuffer.AppendString(session.GetHabbo().Motto);
            messageBuffer.AppendInteger(session.GetHabbo().AchievementPoints);
            room.SendMessage(messageBuffer.GetReversedBytes());

            return true;
        }
    }
}