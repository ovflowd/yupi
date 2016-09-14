namespace Yupi.Messages.User
{
    using System;

    public class HotelViewCountdownMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string time = message.GetString();
            router.GetComposer<HotelViewCountdownMessageComposer>().Compose(session, time);
        }

        #endregion Methods
    }
}