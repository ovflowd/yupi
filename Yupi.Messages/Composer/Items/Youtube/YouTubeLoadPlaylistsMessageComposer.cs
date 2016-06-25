using System;
using Yupi.Protocol;
using Yupi.Emulator.Game.Items.Interfaces;
using System.Collections.Generic;
using Yupi.Emulator.Game.Users;

namespace Yupi.Messages.Youtube
{
	public class YouTubeLoadPlaylistsMessageComposer : AbstractComposer
	{
		public void Compose(ISender session, RoomItem tv, Dictionary<string, YoutubeVideo> videos) {
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tv.Id);
				message.AppendInteger(videos.Count);

				foreach (YoutubeVideo video in videos.Values) {
					video.Serialize (message);
				}
				message.AppendString(tv.ExtraData);

				session.Send (message);
			}
		}
	}
}

