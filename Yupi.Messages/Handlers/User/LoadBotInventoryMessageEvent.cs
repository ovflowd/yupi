namespace Yupi.Messages.User
{
    using System;

    using Yupi.Messages.Bots;

    public class LoadBotInventoryMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            throw new NotImplementedException();
            //router.GetComposer<BotInventoryMessageComposer> ().Compose (session, session.GetHabbo ().GetInventoryComponent ()._inventoryBots);
        }

        #endregion Methods
    }
}