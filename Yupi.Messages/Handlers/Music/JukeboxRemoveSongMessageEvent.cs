namespace Yupi.Messages.Music
{
    using System;
    using System.Linq;

    using Yupi.Messages.Contracts;
    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Domain.Components;
    using Yupi.Model.Repository;

    public class JukeboxRemoveSongMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SongItem> ItemRepository;

        #endregion Fields

        #region Constructors

        public JukeboxRemoveSongMessageEvent()
        {
            ItemRepository = DependencyFactory.Resolve<IRepository<SongItem>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null)
                return;

            int itemId = message.GetInteger();

            SongMachineComponent songMachine = session.Room.Data.SongMachine;

            SongItem item = songMachine.Find(itemId);

            if (item == null)
            {
                return;
            }

            songMachine.Remove(item);
            session.Info.Inventory.FloorItems.Add(item);
            ItemRepository.Save(item);

            SongItem[] items = session.Info.Inventory.FloorItems.OfType<SongItem>().ToArray();

            router.GetComposer<UpdateInventoryMessageComposer>().Compose(session);
            router.GetComposer<SongsLibraryMessageComposer>().Compose(session, items);
            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, songMachine);
        }

        #endregion Methods
    }
}