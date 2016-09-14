namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Protocol.Buffers;

    public abstract class HotelViewHallOfFameMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string code)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}