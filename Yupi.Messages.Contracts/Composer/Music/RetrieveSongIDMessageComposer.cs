namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class RetrieveSongIDMessageComposer : AbstractComposer<string, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string name, int songId)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}