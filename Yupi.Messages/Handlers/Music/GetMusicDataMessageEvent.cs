namespace Yupi.Messages.Music
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Yupi.Model;
    using Yupi.Model.Domain;
    using Yupi.Model.Repository;

    public class GetMusicDataMessageEvent : AbstractHandler
    {
        #region Fields

        private IRepository<SongData> SongRepository;

        #endregion Fields

        #region Constructors

        public GetMusicDataMessageEvent()
        {
            SongRepository = DependencyFactory.Resolve<IRepository<SongData>>();
        }

        #endregion Constructors

        #region Methods

        public override void HandleMessage(Yupi.Model.Domain.Habbo session, Yupi.Protocol.Buffers.ClientMessage message,
            Yupi.Protocol.IRouter router)
        {
            int count = message.GetInteger();

            List<int> songsIds = new List<int>();

            for (int i = 0; i < count; i++)
            {
                int songId = message.GetInteger();
                songsIds.Add(songId);
            }

            var songs = SongRepository.All().Where(x => songsIds.Contains(x.Id)).ToList();

            router.GetComposer<SongsMessageComposer>().Compose(session, songs);
        }

        #endregion Methods
    }
}