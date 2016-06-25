using System;
using Yupi.Emulator.Game.Items.Interfaces;
using System.Collections.Generic;
using Yupi.Emulator.Game.Users;
using Yupi.Messages.Youtube;

namespace Yupi.Messages.Items
{
	public class YouTubeGetPlayerMessageEvent : AbstractHandler
	{
		public override void HandleMessage (Yupi.Emulator.Game.GameClients.Interfaces.GameClient session, Yupi.Protocol.Buffers.ClientMessage request, Router router)
		{
			uint itemId = request.GetUInt32();

			RoomItem item = session.GetHabbo().CurrentRoom.GetRoomItemHandler().GetItem(itemId);

			if (item == null)
				return;

			Dictionary<string, YoutubeVideo> videos = session.GetHabbo().GetYoutubeManager().Videos;

			if (videos == null)
				return;

			router.GetComposer<YouTubeLoadVideoMessageComposer> ().Compose (session, item);
			router.GetComposer<YouTubeLoadPlaylistsMessageComposer> ().Compose (session, item, videos);
		}
	}
}

