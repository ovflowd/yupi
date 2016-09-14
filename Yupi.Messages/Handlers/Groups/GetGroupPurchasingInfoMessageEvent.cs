namespace Yupi.Messages.Groups
{
    using System;

    public class GetGroupPurchasingInfoMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<GroupPurchasePartsMessageComposer>().Compose(session);
        }

        #endregion Methods
    }
}