using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class HeightMapMessageComposer : Yupi.Messages.Contracts.HeightMapMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, HeightMap map)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(map.TotalX);
				message.AppendInteger(map.MapSize);
				for (int i = 0; i< map.MapSize; i++)
				{
					// TODO Why *256?
					message.AppendShort((short)(map.GetTileHeight(i)*256));
				}
				session.Send (message);
			}
		}
	}
}

