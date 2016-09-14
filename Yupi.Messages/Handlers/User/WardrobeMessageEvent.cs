namespace Yupi.Messages.User
{
    using System;
    using System.Data;

    public class WardrobeMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<LoadWardrobeMessageComposer>().Compose(session, session.Info.Inventory.Wardrobe);
        }

        #endregion Methods
    }
}