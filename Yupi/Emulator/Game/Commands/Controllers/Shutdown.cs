using System.Threading.Tasks;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Shutdown. This class cannot be inherited.
    /// </summary>
    internal sealed class Shutdown : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Shutdown" /> class.
        /// </summary>
        public Shutdown()
        {
            MinRank = 9;
            Description = "Shutdown the Server.";
            Usage = ":shutdown";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            Yupi.GetGame()
                .GetModerationTool()
                .LogStaffEntry(session.GetHabbo().UserName, string.Empty, "Shutdown",
                    "Issued shutdown command!");
            new Task(Yupi.PerformShutDown).Start();
            return true;
        }
    }
}