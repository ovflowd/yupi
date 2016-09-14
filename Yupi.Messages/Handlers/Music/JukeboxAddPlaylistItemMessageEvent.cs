namespace Yupi.Messages.Music
{
    using System;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class JukeboxAddPlaylistItemMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SongItem> ItemRepository;

        #endregion Fields

        #region Constructors

        public JukeboxAddPlaylistItemMessageEvent()
        {
            ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null
                || !session.Room.Data.HasOwnerRights(session.Info))
            {
                return;
            }

            int itemId = message.GetInteger();

            SongItem item = session.Info.Inventory.GetFloorItem(itemId) as SongItem;

            if (item == null)
                return;

            SongMachineComponent songMachine = session.Room.Data.SongMachine;

            if (songMachine.TryAdd(item))
            {
                session.Info.Inventory.FloorItems.Remove(item);
                router.GetComposer<RemoveInventoryObjectMessageComposer>().Compose(session, item.Id);

                ItemRepository.Save(item);
            }

            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, songMachine);
        }

        #endregion Methods
    }
}