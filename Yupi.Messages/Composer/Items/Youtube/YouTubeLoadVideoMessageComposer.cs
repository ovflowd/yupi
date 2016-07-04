using System;
using Yupi.Protocol.Buffers;
using Yupi.Emulator.Game.Items.Interfaces;

namespace Yupi.Messages.Youtube
{
	public class YouTubeLoadVideoMessageComposer : AbstractComposer<RoomItem>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomItem tv)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(tv.Id);
				message.AppendString(tv.ExtraData);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

