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
					message.AppendInteger(entity.Position.X);
					message.AppendInteger(entity.Position.Y);
					message.AppendString(entity.Position.Z.ToString(CultureInfo.InvariantCulture));
					message.AppendInteger(entity.RotHead);
					message.AppendInteger(entity.RotBody);
					message.AppendString(string.Empty); // TODO Extra Data
				}

				session.Send (message);
			}
		}
	}
}

