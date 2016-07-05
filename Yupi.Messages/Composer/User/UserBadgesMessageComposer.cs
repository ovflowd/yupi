using System;
using Yupi.Net;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class UserBadgesMessageComposer : AbstractComposer<uint>
	{
		public override void Compose (Yupi.Protocol.ISender session, uint user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (user);
				// TODO Rewrite !!!
				message.StartArray ();
				foreach (
				Badge badge in
				roomUserByHabbo.GetClient()
				.GetHabbo()
				.GetBadgeComponent()
				.BadgeList.Values.Cast<Badge>()
				.Where(badge => badge.Slot > 0)) {
					message.AppendInteger (badge.Slot);
					message.AppendString (badge.Code);

					message.SaveArray ();
				}

				message.EndArray ();

				// TODO Can this event even occur when a user isn't in a room?
				if (session.GetHabbo().InRoom)
					Yupi.GetGame().GetRoomManager().GetRoom(session.GetHabbo().CurrentRoomId).SendMessage(message);
				else
					session.SendMessage(message);
			}
		}
	}
}

