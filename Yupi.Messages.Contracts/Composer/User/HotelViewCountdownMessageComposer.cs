namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class HotelViewCountdownMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string time)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}