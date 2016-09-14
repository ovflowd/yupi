namespace Yupi.Messages.Other
{
    using System;

    public class InitCryptoMessageEvent : AbstractHandler
    {
        #region Properties

        public override bool RequireUser
        {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<InitCryptoMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}