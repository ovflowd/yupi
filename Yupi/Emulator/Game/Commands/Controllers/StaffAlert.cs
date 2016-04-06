using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
     public sealed class StaffAlert : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StaffAlert" /> class.
        /// </summary>
        public StaffAlert()
        {
            MinRank = 5;
            Description = "Alerts to all connected staffs.";
            Usage = ":sa [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string msg = string.Join(" ", pms);

            SimpleServerMessageBuffer messageBuffer =
                new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("staffcloud");
            messageBuffer.AppendInteger(2);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("Staff  Alert");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString(
                $"{msg}\r\n- <i>Sender: {session.GetHabbo().UserName}</i>");
            Yupi.GetGame().GetClientManager().StaffAlert(messageBuffer);
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, string.Empty, "StaffAlert",
                    $"Staff alert [{msg}]");

            return true;
        }
    }
}