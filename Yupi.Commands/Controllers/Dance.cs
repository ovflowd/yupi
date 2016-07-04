using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;



namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Dance. This class cannot be inherited.
    /// </summary>
     public sealed class Dance : Command
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
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer();
            messageBuffer.Init(PacketLibraryManager.OutgoingHandler("DanceStatusMessageComposer"));
            messageBuffer.AppendInteger(session.CurrentRoomUserId);
            messageBuffer.AppendInteger(result);
            session.GetHabbo().CurrentRoom.SendMessage(messageBuffer);

            return true;
        }
    }
}