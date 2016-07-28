using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Collections;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class UpdateFurniStackMapMessageComposer : AbstractComposer<IList<Vector>, RoomData>
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<Vector> affectedTiles, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendByte((byte) affectedTiles.Count);
				foreach (Vector coord in affectedTiles)
				{ // TODO What about coord.Z?
					message.AppendByte((byte) coord.X);
					message.AppendByte((byte) coord.Y);
					message.AppendShort((short) (room.GetGameMap().SqAbsoluteHeight(coord.X, coord.Y)*256));
				}
				session.Send (message);
			}
		}
	}
}

