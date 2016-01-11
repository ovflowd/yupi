using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshNavigator. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshNavigator : Command
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