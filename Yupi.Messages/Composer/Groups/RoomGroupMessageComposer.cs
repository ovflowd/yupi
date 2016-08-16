using System;
using System.Collections.Generic;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;


namespace Yupi.Messages.Groups
{
	public class RoomGroupMessageComposer : Yupi.Messages.Contracts.RoomGroupMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender room, ISet<Group> groups)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(groups.Count);

				foreach (Group current in groups)
				{
					message.AppendInteger(current.Id);
					message.AppendString(current.Badge);
				}

				room.Send (message);
			}
		}
	}
}

