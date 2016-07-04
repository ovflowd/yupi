using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class FollowUser. This class cannot be inherited.
    /// </summary>
     public sealed class FollowUser : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FollowUser" /> class.
        /// </summary>
        public FollowUser()
        {
            MinRank = 1;
            Description = "Follow the selected user.";
            Usage = ":follow [USERNAME]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            GameClient client = Yupi.GetGame().GetClientManager().GetClientByUserName(pms[0]);
            if (client == null || client.GetHabbo() == null)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("user_not_found"));
                return true;
            }
            if (client.GetHabbo().CurrentRoom == null ||
                client.GetHabbo().CurrentRoom == session.GetHabbo().CurrentRoom)
                return false;
            SimpleServerMessageBuffer roomFwd =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("RoomForwardMessageComposer"));
            roomFwd.AppendInteger(client.GetHabbo().CurrentRoom.RoomId);
            session.SendMessage(roomFwd);

            return true;
        }
    }
}