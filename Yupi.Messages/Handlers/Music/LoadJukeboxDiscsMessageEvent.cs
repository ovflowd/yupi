namespace Yupi.Messages.Music
{
    using System;
    using System.Linq;

    using Yupi.Model.Domain;

    public class LoadJukeboxDiscsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            SongItem[] items = session.Info.Inventory.FloorItems.OfType<SongItem>().ToArray();

            router.GetComposer<SongsLibraryMessageComposer>().Compose(session, items);
        }

        #endregion Methods
    }
}