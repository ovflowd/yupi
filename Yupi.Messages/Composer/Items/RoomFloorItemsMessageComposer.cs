using System;
using System.Collections.Generic;



using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Items
{
	public class RoomFloorItemsMessageComposer : AbstractComposer <RoomData, IReadOnlyDictionary<uint, FloorItem>>
	{
		public override void Compose (Yupi.Protocol.ISession<Yupi.Model.Domain.Habbo> session, RoomData data, IReadOnlyDictionary<uint, FloorItem> items)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				if (data.Group != null) {
					if (data.Group.AdminOnlyDeco == 1u) {
						message.AppendInteger (data.Group.Admins.Count + 1);


						foreach (GroupMember member in data.Group.Admins.Values) {
							if (member != null) {
								message.AppendInteger (member.Id);
								message.AppendString (Yupi.GetHabboById (member.Id).UserName);
							}
						}

						message.AppendInteger (data.OwnerId);
						message.AppendString (data.Owner);
					} else {

						message.AppendInteger (data.Group.Members.Count + 1);

						foreach (GroupMember member in data.Group.Members.Values) {
							message.AppendInteger (member.Id);
							message.AppendString (Yupi.GetHabboById (member.Id).UserName);
						}
					}
				} else {
					message.AppendInteger (1);
					message.AppendInteger (data.OwnerId);
					message.AppendString (data.Owner);
				}

				message.AppendInteger (items.Count);
			
				foreach (KeyValuePair<uint, RoomItem> roomItem in items)
					roomItem.Value.Serialize (message);

				session.Send (message);
			}
		}
	}
}

