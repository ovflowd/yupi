namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class UserIsPlayingFreezeMessageComposer : AbstractComposer<bool>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, bool isPlaying)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}