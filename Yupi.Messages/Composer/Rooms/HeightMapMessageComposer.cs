using System;
using Yupi.Emulator.Game.Rooms.User.Path;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class HeightMapMessageComposer : AbstractComposer<Gamemap>
	{
		public override void Compose (Yupi.Protocol.ISender session, Gamemap map)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(map.Model.MapSizeX);
				message.AppendInteger(map.Model.MapSizeX*map.Model.MapSizeY);
				for (int i = 0; i < map.Model.MapSizeY; i++)
				{
					for (int j = 0; j < map.Model.MapSizeX; j++)
					{
						message.AppendShort((short) (map.SqAbsoluteHeight(j, i)*256));
					}
				}
				session.Send (message);
			}
		}
	}
}

