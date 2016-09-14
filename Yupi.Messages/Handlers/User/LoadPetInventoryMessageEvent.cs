namespace Yupi.Messages.User
{
    using System;

    public class LoadPetInventoryMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            session.Router.GetComposer<PetInventoryMessageComposer>().Compose(session, session.Info.Inventory.Pets);
        }

        #endregion Methods
    }
}