using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Contracts
{
    public abstract class JukeboxNowPlayingMessageComposer : AbstractComposer<uint, int, int>
    {
        public override void Compose(Yupi.Protocol.ISender session, uint songId, int playlistPosition, int songPosition)
        {
            // Do nothing by default.
        }
    }
}