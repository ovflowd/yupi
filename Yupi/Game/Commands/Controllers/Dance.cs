using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Dance. This class cannot be inherited.
    /// </summary>
    internal sealed class Dance : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Dance" /> class.
        /// </summary>
        public Dance()
        {
            MinRank = 1;
            Description = "Makes you dance.";
            Usage = ":dance [danceId(0 - 4)]";
            MinParams = 1;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            ushort result;
            ushort.TryParse(pms[0], out result);

            if (result > 4)
            {
                session.SendWhisper(Yupi.GetLanguage().GetVar("command_dance_false"));
                result = 0;
            }
            ServerMessage message = new ServerMessage();
            message.Init(LibraryParser.OutgoingRequest("DanceStatusMessageComposer"));
            message.AppendInteger(session.CurrentRoomUserId);
            message.AppendInteger(result);
            session.GetHabbo().CurrentRoom.SendMessage(message);

            return true;
        }
    }
}