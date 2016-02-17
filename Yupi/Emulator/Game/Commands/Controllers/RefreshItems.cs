using Yupi.Data;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshItems. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshItems : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshItems" /> class.
        /// </summary>
        public RefreshItems()
        {
            MinRank = 9;
            Description = "Refreshes Items from Database.";
            Usage = ":refresh_items";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            FurnitureDataManager.SetCache();
            Yupi.GetGame().ReloadItems();
            FurnitureDataManager.Clear();
            session.SendNotif(Yupi.GetLanguage().GetVar("command_refresh_items"));
            return true;
        }
    }
}