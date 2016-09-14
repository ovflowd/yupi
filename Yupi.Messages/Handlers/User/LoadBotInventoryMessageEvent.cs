using System;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
    public class LoadBotInventoryMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            throw new NotImplementedException();
            //router.GetComposer<BotInventoryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ()._inventoryBots);
        }
    }
}