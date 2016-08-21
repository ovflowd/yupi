using System;
using Yupi.Protocol.Buffers;
using System.Collections.Generic;
using System.Collections;
using Yupi.Model.Domain.Components;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class UpdateFurniStackMapMessageComposer : Yupi.Messages.Contracts.UpdateFurniStackMapMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, IList<Vector3D> affectedTiles, RoomData room)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendByte((byte) affectedTiles.Count);
				foreach (Vector3D coord in affectedTiles)
				{ // TODO What about coord.Z?
					message.AppendByte((byte) coord.X);
					message.AppendByte((byte) coord.Y);
					throw new NotImplementedException ();
				//	message.AppendShort((short) (room.GetGameMap().SqAbsoluteHeight(coord.X, coord.Y)*256));
				}
				session.Send (message);
			}
		}
	}
}

