using System.Linq;
using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class JukeboxRemoveSongMessageEvent : AbstractHandler
    {
        private readonly IRepository<SongItem> ItemRepository;

        public JukeboxRemoveSongMessageEvent()
        {
            ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if (session.Room == null)
                return;

            var itemId = message.GetInteger();

            var songMachine = session.Room.Data.SongMachine;

            var item = songMachine.Find(itemId);

            if (item == null) return;

            songMachine.Remove(item);
            session.Info.Inventory.FloorItems.Add(item);
            ItemRepository.Save(item);

            var items = session.Info.Inventory.FloorItems.OfType<SongItem>().ToArray();

            router.GetComposer<UpdateInventoryMessageComposer>().Compose(session);
            router.GetComposer<SongsLibraryMessageComposer>().Compose(session, items);
            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, songMachine);
        }
    }
}