using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class RoomErrorMessageComposer : AbstractComposer<int>
	{
		public override void Compose ( Yupi.Protocol.ISender session, int errorCode)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (errorCode); 
				session.Send (message);
			}
		}
	}
}

