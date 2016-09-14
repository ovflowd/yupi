namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class GeneralErrorHabboMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int errorId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}