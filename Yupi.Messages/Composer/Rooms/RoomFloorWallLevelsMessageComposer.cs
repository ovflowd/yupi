using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class RoomFloorWallLevelsMessageComposer : AbstractComposer<RoomData>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, RoomData data)
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

