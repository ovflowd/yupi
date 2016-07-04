using System;
using Yupi.Emulator.Game.Rooms.Data.Models;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomFloorWallLevelsMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISender session, RoomData data)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendBool(data.HideWall);
				message.AppendInteger(data.WallThickness);
				message.AppendInteger(data.FloorThickness);
				session.Send (message);
			}
		}
	}
}

