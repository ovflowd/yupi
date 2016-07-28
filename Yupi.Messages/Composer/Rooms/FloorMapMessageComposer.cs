using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class FloorMapMessageComposer : AbstractComposer<string, int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, string heightmap, int wallHeight)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(true);
				message.AppendInteger(wallHeight);
				message.AppendString(heightmap);
				session.Send (message);
			}
		}
	}
}

