using System;
using System.Collections.Generic;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Groups
{
	public class GroupPurchasePageMessageComposer : AbstractComposer<HashSet<RoomData>>
	{
		public override void Compose ( Yupi.Protocol.ISender session, HashSet<RoomData> rooms)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {

				// TODO Hardcoded message

				message.AppendInteger(10);
				message.AppendInteger(rooms.Count);

				foreach (RoomData room in rooms)
				{
					message.AppendInteger(room.Id);
					message.AppendString(room.Name);
					message.AppendBool(false);
				}

				message.AppendInteger(5);
				message.AppendInteger(10);
				message.AppendInteger(3);
				message.AppendInteger(4);
				message.AppendInteger(25);
				message.AppendInteger(17);
				message.AppendInteger(5);
				message.AppendInteger(25);
				message.AppendInteger(17);
				message.AppendInteger(3);
				message.AppendInteger(29);
				message.AppendInteger(11);
				message.AppendInteger(4);
				message.AppendInteger(0);
				message.AppendInteger(0);
				message.AppendInteger(0);
				session.Send (message);
			}
		}
	}
}

