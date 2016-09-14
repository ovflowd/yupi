namespace Yupi.Messages.User
{
    using System;

    public class GetCameraPriceMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            // TODO Replace hardcoded values
            router.GetComposer<SetCameraPriceMessageComposer>().Compose(session, 0, 10);
        }

        #endregion Methods
    }
}