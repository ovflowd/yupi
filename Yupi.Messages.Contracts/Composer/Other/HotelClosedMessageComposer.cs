namespace Yupi.Messages.Contracts
{
    using System;

    public class HotelClosedMessageComposer : AbstractComposer<int, int, bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int openHour, int openMinute, bool userThrownOut)
        {
        }

        #endregion Methods
    }
}