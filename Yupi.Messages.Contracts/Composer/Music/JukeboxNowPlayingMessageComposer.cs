using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class JukeboxNowPlayingMessageComposer : AbstractComposer<uint, int, int>
    {
        public override void Compose(ISender session, uint songId, int playlistPosition, int songPosition)
        {
            // Do nothing by default.
        }
    }
}