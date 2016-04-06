using Yupi.Emulator.Data;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshItems. This class cannot be inherited.
    /// </summary>
     public sealed class RefreshItems : Command
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