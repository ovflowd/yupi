namespace Yupi.Messages.Music
{
    using System;

    public class GetJukeboxPlaylistsMessageEvent : AbstractHandler
    {
        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            if (session.Room == null)
                return;

            router.GetComposer<JukeboxPlaylistMessageComposer>().Compose(session, session.Room.Data.SongMachine);
        }

        #endregion Methods
    }
}