using Yupi.Data;
using Yupi.Data.Base.Adapters.Interfaces;
using Yupi.Game.Commands.Interfaces;
using Yupi.Game.GameClients.Interfaces;
using Yupi.Messages;
using Yupi.Messages.Parsers;

namespace Yupi.Game.Commands.Controllers
{
    /// <summary>
    ///     Class LTD. This class cannot be inherited.
    /// </summary>
    internal sealed class Ltd : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Ltd" /> class.
        /// </summary>
        public Ltd()
        {
            MinRank = 7;
            Description = "Atualiza os LTDS";
            Usage = ":ltd";
            MinParams = 0;
        }

        public override bool Execute(GameClient session, string[] pms)
        {
            using (IQueryAdapter adapter = Yupi.GetDatabaseManager().GetQueryReactor())
            {
                FurnitureDataManager.SetCache();
                Yupi.GetGame().GetItemManager().LoadItems(adapter);
                Yupi.GetGame().GetCatalog().Initialize(adapter);
                Yupi.GetGame().ReloadItems();
                FurnitureDataManager.Clear();
            }
            Yupi.GetGame()
                .GetClientManager()
                .QueueBroadcaseMessage(
                    new ServerMessage(LibraryParser.OutgoingRequest("PublishShopMessageComposer")));
            ServerMessage message = new ServerMessage(LibraryParser.OutgoingRequest("SuperNotificationMessageComposer"));
            message.AppendString("ninja_promo_LTD");
            message.AppendInteger(4);
            message.AppendString("title");
            message.AppendString("Novo Raro Limitado!");
            message.AppendString("message");
            message.AppendString(
                "<i><h1>Como Assim?</h1>, Um Novo Raro Limitado foi Adicionado na Loja!<br> Descubra como ele é Abrindo a Loja!</br>");
            message.AppendString("linkUrl");
            message.AppendString("event:catalog/open/ultd_furni");
            message.AppendString("linkTitle");
            message.AppendString("Ver o Raro");

            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(message);
            return true;
        }
    }
}