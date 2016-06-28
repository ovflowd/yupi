using Yupi.Emulator.Data;
using Yupi.Emulator.Data.Base.Adapters.Interfaces;
using Yupi.Emulator.Game.Commands.Interfaces;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Emulator.Messages;


namespace Yupi.Emulator.Game.Commands.Controllers
{
    /// <summary>
    ///     Class LTD. This class cannot be inherited.
    /// </summary>
     public sealed class Ltd : Command
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
                Yupi.GetGame().GetCatalogManager().Init(adapter);
                Yupi.GetGame().ReloadItems();
                FurnitureDataManager.Clear();
            }
            Yupi.GetGame()
                .GetClientManager()
                .QueueBroadcaseMessage(
                    new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("PublishShopMessageComposer")));
            SimpleServerMessageBuffer messageBuffer = new SimpleServerMessageBuffer(PacketLibraryManager.OutgoingHandler("SuperNotificationMessageComposer"));
            messageBuffer.AppendString("ninja_promo_LTD");
            messageBuffer.AppendInteger(4);
            messageBuffer.AppendString("title");
            messageBuffer.AppendString("Novo Raro Limitado!");
            messageBuffer.AppendString("message");
            messageBuffer.AppendString(
                "<i><h1>Como Assim?</h1>, Um Novo Raro Limitado foi Adicionado na Loja!<br> Descubra como ele é Abrindo a Loja!</br>");
            messageBuffer.AppendString("linkUrl");
            messageBuffer.AppendString("event:catalog/open/ultd_furni");
            messageBuffer.AppendString("linkTitle");
            messageBuffer.AppendString("Ver o Raro");

            Yupi.GetGame().GetClientManager().QueueBroadcaseMessage(messageBuffer);
            return true;
        }
    }
}