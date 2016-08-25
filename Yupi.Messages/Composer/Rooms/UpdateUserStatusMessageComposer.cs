using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using Yupi.Messages.Encoders;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

namespace Yupi.Messages.Rooms
{
	public class UpdateUserStatusMessageComposer : Contracts.UpdateUserStatusMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, IList<Yupi.Model.Domain.RoomEntity> entities)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(entities.Count);

				foreach (RoomEntity entity in entities) {
					message.AppendInteger(entity.Id);
					message.Append(entity.Position);
					message.AppendInteger(entity.RotHead);
					message.AppendInteger(entity.RotBody);
					message.AppendString(string.Empty); // TODO Extra Data
				}

				session.Send (message);
			}
		}
	}
}

