using Yupi.Model.Domain.Components;
using Yupi.Protocol;

namespace Yupi.Messages.Music
{
    public class JukeboxPlaylistMessageComposer : Contracts.JukeboxPlaylistMessageComposer
    {
        public override void Compose(ISender session, SongMachineComponent songMachine)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(songMachine.Capacity);

                message.AppendInteger(songMachine.SongItems.Count);

                foreach (var current in songMachine.SongItems)
                {
                    message.AppendInteger(current.Id);
                    message.AppendInteger(current.Song.Id);
                }
                session.Send(message);
            }
        }
    }
}