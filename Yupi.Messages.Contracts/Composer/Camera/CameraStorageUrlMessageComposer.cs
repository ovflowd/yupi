namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class CameraStorageUrlMessageComposer : AbstractComposer<string>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, string url)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}