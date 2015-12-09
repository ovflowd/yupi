using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.SoundMachine;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshSongs. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshSongs : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshSongs" /> class.
        /// </summary>
        public RefreshSongs()
        {
            MinRank = 9;
            Description = "Refreshes Songs from Database.";
            Usage = ":refresh_songs";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            SoundMachineSongManager.Initialize();
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_songs"));
            return true;
        }
    }
}