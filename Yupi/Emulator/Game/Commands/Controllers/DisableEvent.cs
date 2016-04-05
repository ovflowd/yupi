using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Sit. This class cannot be inherited.
    /// </summary>
     sealed class DisableEvent : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Sit" /> class.
        /// </summary>
        public DisableEvent()
        {
            MinRank = 9;
            Description = "Desativa as mensagens de Eventos do Hotel";
            Usage = ":disableevent";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            session.GetHabbo().DisableEventAlert = !session.GetHabbo().DisableEventAlert;
            return true;
        }
    }
}