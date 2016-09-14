using Yupi.Protocol;

namespace Yupi.Messages.Music
{
    // TODO Refactor?
    public class JukeboxNowPlayingMessageComposer : Contracts.JukeboxNowPlayingMessageComposer
    {
        public override void Compose(ISender session, uint songId, int playlistPosition, int songPosition)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(songId);
                message.AppendInteger(playlistPosition);
                message.AppendInteger(songId);
                message.AppendInteger(0);
                message.AppendInteger(songPosition); // songPosition in ms
                session.Send(message);
            }
        }
    }
}