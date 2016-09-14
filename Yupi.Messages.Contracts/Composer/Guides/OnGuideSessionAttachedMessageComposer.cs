namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class OnGuideSessionAttachedMessageComposer : AbstractComposer<bool, int, string, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool val1, int userId, string message, int val2)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}