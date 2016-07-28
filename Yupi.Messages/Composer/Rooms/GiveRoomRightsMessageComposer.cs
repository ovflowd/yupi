using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class GiveRoomRightsMessageComposer : AbstractComposer<uint, Habbo>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, uint roomId, Habbo habbo)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(roomId);
				message.AppendInteger(habbo.Id);
				message.AppendString(habbo.UserName);
				session.Send (message);
			}
		}
	}
}

