using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class GetJukeboxPlaylistsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (session.Room == null)
                return;

            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, session.Room.Data.SongMachine);
        }
    }
}