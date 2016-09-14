using Yupi.Model;
using Yupi.Model.Domain;
using Yupi.Model.Repository;
using Yupi.Protocol;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Music
{
    public class RetrieveSongIDMessageEvent : AbstractHandler
    {
        private readonly IRepository<SongData> SongRepository;

        public RetrieveSongIDMessageEvent()
        {
            SongRepository = DependencyFactory.Resolve<IRepository<SongData>>();
        }

        public override void HandleMessage(Habbo session, ClientMessage message, IRouter router)
        {
            var name = message.GetString();

            var song = SongRepository.FindBy(x => x.CodeName == name);

            if (song != null) router.GetComposer<RetrieveSongIDMessageComposer>().Compose(session, name, song.Id);
        }
    }
}