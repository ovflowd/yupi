using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshNavigator. This class cannot be inherited.
    /// </summary>
     sealed class RefreshNavigator : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshNavigator" /> class.
        /// </summary>
        public RefreshNavigator()
        {
            MinRank = 9;
            Description = "Refreshes navigator from Database.";
            Usage = ":refresh_navigator";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                Yupi.GetGame().GetNavigator().Initialize(adapter);
                Yupi.GetGame().GetRoomManager().LoadModels(adapter);
            }

            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_navigator"));
            return true;
        }
    }
}