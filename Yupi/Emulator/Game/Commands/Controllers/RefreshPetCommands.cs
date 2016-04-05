using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Game.Pets;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshBotCommands. This class cannot be inherited.
    /// </summary>
     sealed class RefreshPetCommands : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SetVideos" /> class.
        /// </summary>
        public RefreshPetCommands()
        {
            MinRank = 7;
            Description = "Refresh Pet Commands";
            Usage = ":refresh_pet_commands";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            PetTypeManager.Load();
            PetCommandHandler.Init(Yupi.GetDatabaseManager().GetQueryReactor());

            return true;
        }
    }
}