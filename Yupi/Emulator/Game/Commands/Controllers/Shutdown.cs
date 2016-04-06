using System.Threading.Tasks;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class Shutdown. This class cannot be inherited.
    /// </summary>
     public sealed class Shutdown : Command
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