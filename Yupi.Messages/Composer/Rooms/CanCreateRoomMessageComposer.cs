using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class CanCreateRoomMessageComposer : AbstractComposerVoid
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				// TODO Refactor
				message.AppendInteger(session.GetHabbo().UsersRooms.Count >= 75 ? 1 : 0);
				message.AppendInteger(75);
				session.Send (message);
			}
		}
	}
}

