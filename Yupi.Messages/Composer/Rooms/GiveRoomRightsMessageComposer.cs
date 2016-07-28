using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Rooms
{
	public class GiveRoomRightsMessageComposer : AbstractComposer<uint, UserInfo>
	{
		public override void Compose ( Yupi.Protocol.ISender session, uint roomId, UserInfo habbo)
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

