namespace Yupi.Messages.User
{
    using System;

    using Yupi.Protocol.Buffers;

    public class SetCameraPriceMessageComposer : Yupi.Messages.Contracts.SetCameraPriceMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int credits, int seasonalCurrency)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(credits);
                message.AppendInteger(seasonalCurrency);
                session.Send(message);
            }
        }

        #endregion Methods
    }
}