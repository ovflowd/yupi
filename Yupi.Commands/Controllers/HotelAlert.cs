using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
     public sealed class HotelAlert : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="HotelAlert" /> class.
        /// </summary>
        public HotelAlert()
        {
            MinRank = 5;
            Description = "Alerts the whole Hotel.";
            Usage = ":ha [MESSAGE]";
            MinParams = -1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            string str = string.Join(" ", pms);
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("BroadcastNotifMessageComposer"));
            messageBuffer.AppendString($"{str}\r\n- {session.GetHabbo().UserName}");
            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(messageBuffer);

            return true;
        }
    }
}