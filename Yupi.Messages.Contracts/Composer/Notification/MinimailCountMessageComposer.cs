namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class MinimailCountMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int count)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}