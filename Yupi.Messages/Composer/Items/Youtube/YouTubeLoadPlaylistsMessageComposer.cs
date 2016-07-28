using System;
using Yupi.Protocol;

using System.Collections.Generic;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Youtube
{
	public class YouTubeLoadPlaylistsMessageComposer : AbstractComposer
	{
		public void Compose(Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, YoutubeTVItem tv, Dictionary<string, YoutubeVideo> videos) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tv.Id);
				message.AppendInteger(videos.Count);

				foreach (YoutubeVideo video in videos.Values) {
					message.AppendString(video.Id);
					message.AppendString(video.Name);
					message.AppendString(video.Description);
				}

				message.AppendString(tv.PlayingVideo.Id);

				session.Send (message);
			}
		}
	}
}

