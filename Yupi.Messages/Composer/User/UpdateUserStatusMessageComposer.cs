using System;

using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Globalization;

namespace Yupi.Messages.User
{
	public class UpdateUserStatusMessageComposer : AbstractComposer<List<RoomEntity>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, List<RoomEntity> entities)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(entities.Count);

				foreach (RoomEntity entity in entities)
					entity.SerializeStatus(message); // RoomUser::SerializeStatus
				session.Send (message);
			}
		}

		public void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, RoomEntity user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(1);
				user.SerializeStatus(message);
				session.Send (message);
			}
		}
	}
}

