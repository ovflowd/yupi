using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.SoundMachine;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshSongs. This class cannot be inherited.
    /// </summary>
     sealed class RefreshSongs : Command
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
            SoundMachineSongManager.Load();
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_songs"));
            return true;
        }
    }
}