using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pets;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshBotCommands. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshPetSpeeches : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SetVideos" /> class.
        /// </summary>
        public RefreshPetSpeeches()
        {
            MinRank = 7;
            Description = "Refresh Pet Speeches";
            Usage = ":refresh_pet_speeches";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            PetLocale.Init(Yupi.GetDatabaseManager().GetQueryReactor());
            return true;
        }
    }
}