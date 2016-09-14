using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.User
{
	public class UserUpdateNameInRoomMessageComposer : Yupi.Messages.Contracts.UserUpdateNameInRoomMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, Habbo habbo)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (habbo.Info.Id);
				message.AppendInteger (habbo.Room.Data.Id);
				message.AppendString (habbo.Info.Name);
				room.Send (message);
			}
		}
	}
}

