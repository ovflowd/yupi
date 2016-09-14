using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SongsLibraryMessageComposer : AbstractComposer<SongItem[]>
    {
        public override void Compose(ISender session, SongItem[] songs)
        {
            // Do nothing by default.
        }
    }
}