using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Rooms;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RoomUnMute. This class cannot be inherited.
    /// </summary>
     sealed class RoomUnMute : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoomUnMute" /> class.
        /// </summary>
        public RoomUnMute()
        {
            MinRank = 5;
            Description = "UnMutes the whole room.";
            Usage = ":roomunmute";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Room room = session.GetHabbo().CurrentRoom;
            if (!session.GetHabbo().CurrentRoom.RoomMuted)
            {
                session.SendWhisper("Room isn't muted.");
                return true;
            }

            session.GetHabbo().CurrentRoom.RoomMuted = false;

            /*
            var message = new SimpleServerMessageBuffer();
            message.Load(PacketLibraryManager.OutgoingHandler("AlertNotificationMessageComposer"));
            message.AppendString("Room is now UnMuted.");
            message.AppendString("");
            room.SendMessage(message);*/

            room.SendMessage(GameClient.GetBytesNotif("Este quarto foi des-selenciado."));

            Yupi.GetGame()
                .GetModerationTool().LogStaffEntry(session.GetHabbo().UserName, string.Empty,
                    "Room Unmute", "Room UnMuted");
            return true;
        }
    }
}