namespace Yupi.Messages.Contracts
{
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public abstract class YouTubeLoadPlaylistsMessageComposer : AbstractComposer<YoutubeTVItem, Dictionary<string, YoutubeVideo>>
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, YoutubeTVItem tv,
            Dictionary<string, YoutubeVideo> videos)
        {
            // Do nothing by default.
        }

        #endregion Methods
    }
}