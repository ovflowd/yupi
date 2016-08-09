using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class PickUpFloorItemMessageComposer : Yupi.Messages.Contracts.PickUpFloorItemMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, FloorItem item, int pickerId)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(item.Id.ToString());
				message.AppendBool(false); //expired
				message.AppendInteger(pickerId); 
				message.AppendInteger(0); // delay
				session.Send (message);
			}
		}
	}
}

