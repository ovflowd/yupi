using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class SongsMessageComposer : AbstractComposer<List<SongData>>
    {
        public override void Compose(ISender session, List<SongData> songs)
        {
            // Do nothing by default.
        }
    }
}