using System;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Rooms
{
	public class SetFloorPlanDoorMessageComposer : Yupi.Messages.Contracts.SetFloorPlanDoorMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, int x, int y, int direction)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (x);
				message.AppendInteger (y);
				message.AppendInteger (direction);
				session.Send (message);
			}
		}
	}
}

