namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class BuildersClubUpdateFurniCountMessageComposer : AbstractComposer<int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int itemsUsed)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}