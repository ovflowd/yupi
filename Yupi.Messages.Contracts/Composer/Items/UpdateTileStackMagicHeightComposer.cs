namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UpdateTileStackMagicHeightComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int itemId, int z)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}