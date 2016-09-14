using System.Collections.Generic;
using System.Linq;
using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class GetMusicDataMessageEvent : AbstractHandler
    {
        private readonly IRepository<SongData> SongRepository;

        public GetMusicDataMessageEvent()
        {
            SongRepository = DependencyFactory.Resolve<IRepository<SongData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var count = message.GetInteger();

            var songsIds = new List<int>();

            for (var i = 0; i < count; i++)
            {
                var songId = message.GetInteger();
                songsIds.Add(songId);
            }

            var songs = SongRepository.All().Where(x => songsIds.Contains(x.Id)).ToList();

            router.GetComposer<SongsMessageComposer>().Compose(session, songs);
        }
    }
}