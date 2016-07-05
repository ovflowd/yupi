using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;


using System.Collections;

namespace Yupi.Messages.Rooms
{
	public class UpdateFurniStackMapMessageComposer : AbstractComposer<ICollection, Room>
	{
		public override void Compose (Yupi.Protocol.ISender session, ICollection affectedTiles, Room room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendByte((byte) affectedTiles.Count);
				foreach (ThreeDCoord coord in affectedTiles.Values)
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

