namespace Yupi.Messages.Contracts
{
    using Yupi.Protocol.Buffers;

    public abstract class JukeboxNowPlayingMessageComposer : AbstractComposer<uint, int, int>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, uint songId, int playlistPosition, int songPosition)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}