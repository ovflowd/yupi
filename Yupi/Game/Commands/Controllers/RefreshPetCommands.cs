using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Game.Pets;
using Yupi.Game.RoomBots;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshBotCommands. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshPetCommands : Command
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