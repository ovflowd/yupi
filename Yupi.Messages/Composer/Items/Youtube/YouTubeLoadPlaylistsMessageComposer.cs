using System;
using Yupi.Protocol;
using Yupi.Emulator.Game.Items.Interfaces;
using System.Collections.Generic;
using Yupi.Emulator.Game.Users;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Youtube
{
	public class YouTubeLoadPlaylistsMessageComposer : AbstractComposer
	{
		public void Compose(ISender session, RoomItem tv, Dictionary<string, YoutubeVideo> videos) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tv.Id);
				message.AppendInteger(videos.Count);

				foreach (YoutubeVideo video in videos.Values) {
					message.AppendString(video.VideoId);
					message.AppendString(video.Name);
					message.AppendString(video.Description);
				}

				message.AppendString(tv.ExtraData);

				session.Send (message);
			}
		}
	}
}

