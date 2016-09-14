namespace Yupi.Messages.Catalog
{
    using System;

    public class GetCatalogClubPageMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int windowId = message.GetInteger();
            router.GetComposer<CatalogueClubPageMessageComposer>().Compose(session, windowId);
        }

        #endregion Methods
    }
}