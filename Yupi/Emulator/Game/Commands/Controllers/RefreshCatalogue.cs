using Yupi.Emulator.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;
using Yupi.Emulator.Messages.Buffers;

namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class RefreshCatalogue. This class cannot be inherited.
    /// </summary>
    internal sealed class RefreshCatalogue : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RefreshCatalogue" /> class.
        /// </summary>
        public RefreshCatalogue()
        {
            MinRank = 9;
            Description = "Refreshes Catalogue from Database.";
            Usage = ":refresh_catalogue";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                FurnitureDataManager.SetCache();
                Yupi.GetGame().GetItemManager().LoadItems(adapter);
                Yupi.GetGame().GetCatalogManager().Init(adapter);
                FurnitureDataManager.Clear();
            }

            Yupi.GetGame()
                .GetClientManager()
                .QueueBroadcaseMessage(new SimpleServerMessageBuffer(PacketLibraryManager.SendRequest("PublishShopMessageComposer")));

            return true;
        }
    }
}