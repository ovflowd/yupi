namespace Yupi.Messages.Pets
{
    using System;

    public class GetSellablePetBreedsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string type = request.GetString();
            router.GetComposer<SellablePetBreedsMessageComposer>().Compose(session, type);
        }

        #endregion Methods
    }
}