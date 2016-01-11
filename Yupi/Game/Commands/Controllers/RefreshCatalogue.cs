using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
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
                Yupi.GetGame().GetCatalog().Initialize(adapter);
                FurnitureDataManager.Clear();
            }

            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(new ServerMessage(LibraryParser.OutgoingRequest("PublishShopMessageComposer")));

            return true;
        }
    }
}