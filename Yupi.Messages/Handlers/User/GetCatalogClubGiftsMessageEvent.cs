namespace Yupi.Messages.User
{
    using System;

    public class GetCatalogClubGiftsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<LoadCatalogClubGiftsMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}