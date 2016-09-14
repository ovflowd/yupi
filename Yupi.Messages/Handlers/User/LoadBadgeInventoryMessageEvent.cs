namespace Yupi.Messages.User
{
    using System;

    public class LoadBadgeInventoryMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            router.GetComposer<LoadBadgesWidgetMessageComposer>().Compose(session, session.Info.Badges);
        }

        #endregion Methods
    }
}