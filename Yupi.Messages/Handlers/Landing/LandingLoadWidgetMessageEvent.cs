namespace Yupi.Messages.Landing
{
    using System;

    public class LandingLoadWidgetMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage request,
            Yupi.Protocol.IRouter router)
        {
            string text = request.GetString();

            router.GetComposer<LandingWidgetMessageComposer>().Compose(session, text);
        }

        #endregion Methods
    }
}