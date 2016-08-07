using System;
using Yupi.Net;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Linq;

namespace Yupi.Messages.User
{
	public class UserBadgesMessageComposer : Yupi.Messages.Contracts.UserBadgesMessageComposer
	{
		public override void Compose ( Yupi.Protocol.ISender session, Habbo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (user.Info.Id);
				var badges = user.Info.Badges.Where(x => x.Slot > 0).ToList();

				message.AppendInteger (badges.Count);

				foreach (Badge badge in badges) {
					message.AppendInteger (badge.Slot);
					message.AppendString (badge.Code);
				}

				// TODO Can this event even occur when a user isn't in a room?
				if (user.Room != null) {
					user.Room.Send (message);
				} else {
					session.Send (message);
				}
			}
		}
	}
}

