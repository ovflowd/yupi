using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
	public class UserUpdateNameInRoomMessageComposer : AbstractComposer<Habbo, string>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> room, Habbo habbo, string newName)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habbo.Id);
				message.AppendInteger (habbo.CurrentRoom.RoomId);
				message.AppendString (newName);
				room.Send (message);
			}
		}
	}
}

