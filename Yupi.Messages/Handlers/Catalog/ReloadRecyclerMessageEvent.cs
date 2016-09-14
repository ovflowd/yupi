namespace Yupi.Messages.Catalog
{
    using System;

    public class ReloadRecyclerMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<ReloadEcotronMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}