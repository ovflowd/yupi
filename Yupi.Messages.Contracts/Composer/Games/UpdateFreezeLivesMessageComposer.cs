namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UpdateFreezeLivesMessageComposer : AbstractComposer<int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, int roomId, int lives)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}