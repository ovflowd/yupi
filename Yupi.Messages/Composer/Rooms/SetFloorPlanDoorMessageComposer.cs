using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.Rooms
{
	public class SetFloorPlanDoorMessageComposer : Yupi.Messages.Contracts.SetFloorPlanDoorMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, Vector3D doorPos, int direction)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (doorPos.X);
				message.AppendInteger (doorPos.Y);
				message.AppendInteger (direction);
				session.Send (message);
			}
		}
	}
}

