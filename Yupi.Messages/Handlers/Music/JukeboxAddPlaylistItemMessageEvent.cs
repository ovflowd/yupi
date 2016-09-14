using Yupi.Messages.Contracts;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class JukeboxAddPlaylistItemMessageEvent : AbstractHandler
    {
        private readonly IRepository<SongItem> ItemRepository;

        public JukeboxAddPlaylistItemMessageEvent()
        {
            ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            if ((session.Room == null)
                || !session.Room.Data.HasOwnerRights(session.Info)) return;

            var itemId = message.GetInteger();

            var item = session.Info.Inventory.GetFloorItem(itemId) as SongItem;

            if (item == null)
                return;

            var songMachine = session.Room.Data.SongMachine;

            if (songMachine.TryAdd(item))
            {
                session.Info.Inventory.FloorItems.Remove(item);
                router.GetComposer<RemoveInventoryObjectMessageComposer>().Compose(session, item.Id);

                ItemRepository.Save(item);
            }

            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, songMachine);
        }
    }
}