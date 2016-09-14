using System.Linq;
using Yupi.Model.Domain;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class LoadJukeboxDiscsMessageEvent : AbstractHandler
    {
        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var items = session.Info.Inventory.FloorItems.OfType<SongItem>().ToArray();

            router.GetComposer<SongsLibraryMessageComposer>().Compose(session, items);
        }
    }
}