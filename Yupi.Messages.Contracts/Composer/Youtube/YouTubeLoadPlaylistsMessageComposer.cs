using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Contracts
{
    public abstract class YouTubeLoadPlaylistsMessageComposer :
        AbstractComposer<YoutubeTVItem, Dictionary<string, YoutubeVideo>>
    {
        public override void Compose(ISender session, YoutubeTVItem tv, Dictionary<string, YoutubeVideo> videos)
        {
            // Do nothing by default.
        }
    }
}