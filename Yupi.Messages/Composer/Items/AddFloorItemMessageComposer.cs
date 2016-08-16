using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class AddFloorItemMessageComposer : Yupi.Messages.Contracts.AddFloorItemMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, FloorItem item)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				/*
				item.Serialize(message);
				message.AppendString(room.Data.Owner.UserName);
				*/
				throw new NotImplementedException ();
				room.Send (message);
			}
		}
	}
}

