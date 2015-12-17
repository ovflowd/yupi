using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class HotelAlert. This class cannot be inherited.
    /// </summary>
    internal sealed class HotelAlert : Command
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
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("BroadcastNotifMessageComposer"));
            message.AppendString($"{str}\r\n- {session.GetHabbo().UserName}");
            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(message);

            return true;
        }
    }
}