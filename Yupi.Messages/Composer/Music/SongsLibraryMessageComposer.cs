using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Music
{
    public class SongsLibraryMessageComposer : Contracts.SongsLibraryMessageComposer
    {
        public override void Compose(ISender session, SongItem[] songs)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(songs.Length);
                foreach (var userItem in songs)
                {
                    message.AppendInteger(userItem.Id);
                    message.AppendInteger(userItem.Song.Id);
                }
                session.Send(message);
            }
        }
    }
}