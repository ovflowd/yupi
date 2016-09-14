namespace Yupi.Messages.User
{
    using System;

    public class GetHotelViewHallOfFameMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            string code = message.GetString();

            router.GetComposer<HotelViewHallOfFameMessageComposer>().Compose(session, code);
        }

        #endregion Methods
    }
}