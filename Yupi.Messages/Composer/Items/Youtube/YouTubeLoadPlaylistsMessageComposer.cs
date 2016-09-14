using System.Collections.Generic;
using Yupi.Model.Domain;
using Yupi.Protocol;

namespace Yupi.Messages.Youtube
{
    public class YouTubeLoadPlaylistsMessageComposer : Contracts.YouTubeLoadPlaylistsMessageComposer
    {
        public override void Compose(ISender session, YoutubeTVItem tv, Dictionary<string, YoutubeVideo> videos)
        {
            using (var message = Pool.GetMessageBuffer(Id))
            {
                message.AppendInteger(tv.Id);
                message.AppendInteger(videos.Count);

                foreach (var video in videos.Values)
                {
                    message.AppendString(video.Id);
                    message.AppendString(video.Name);
                    message.AppendString(video.Description);
                }

                message.AppendString(tv.PlayingVideo.Id);

                session.Send(message);
            }
        }
    }
}