using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Sit. This class cannot be inherited.
    /// </summary>
    internal sealed class DisableEvent : Command
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