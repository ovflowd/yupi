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
				message.AppendInteger(map.Map.Length);  // Width * Height
				for (int i = 0; i< map.Map.Length; i++)
				{
					message.AppendShort((short)(map.Map[i] == 'x' ? -256 : 0));
				}
				session.Send (message);
			}
		}
	}
}

