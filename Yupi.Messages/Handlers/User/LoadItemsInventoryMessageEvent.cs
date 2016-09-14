namespace Yupi.Messages.User
{
    using System;

    using Yupi.Messages.Items;
    using Yupi.Messages.Notification;
    using Yupi.Model.Domain.Components;

    public class LoadItemsInventoryMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<LoadInventoryMessageComposer>().Compose(session, session.Info.Inventory);
        }

        #endregion Methods
    }
}