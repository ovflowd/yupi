namespace Yupi.Messages.Youtube
{
    using System;
    using System.Collections.Generic;

    using Yupi.Model.Domain;
    using Yupi.Protocol;
    using Yupi.Protocol.Buffers;

    public class YouTubeLoadPlaylistsMessageComposer : Yupi.Messages.Contracts.YouTubeLoadPlaylistsMessageComposer
    {
        #region Methods

        public override void Compose(Yupi.Protocol.ISender session, YoutubeTVItem tv,
            Dictionary<string, YoutubeVideo> videos)
        {
            using (ServerMessage message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(tv.Id);
                message.AppendInteger(videos.Count);

                foreach (YoutubeVideo video in videos.Values)
                {
                    message.AppendString(video.Id);
                    message.AppendString(video.Name);
                    message.AppendString(video.Description);
                }

                message.AppendString(tv.PlayingVideo.Id);

                session.Send(message);
            }
        }

        #endregion Methods
    }
}