namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class HelperToolConfigurationMessageComposer : AbstractComposer<bool, int, int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool onDuty, int guideCount, int helperCount,
            int guardianCount)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}