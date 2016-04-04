using System;
using Yupi.Net;
using Yupi.Emulator.Game.GameClients.Interfaces;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class UserBadgesMessageComposer : AbstractComposer<uint>
	{
		public override void Compose (ISession<GameClient> session, uint user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (user);
				// TODO Refactor
				message.StartArray ();
				foreach (
				Badge badge in
				roomUserByHabbo.GetClient()
				.GetHabbo()
				.GetBadgeComponent()
				.BadgeList.Values.Cast<Badge>()
				.Where(badge => badge.Slot > 0).Take(5)) {
					message.AppendInteger (badge.Slot);
					message.AppendString (badge.Code);

					message.SaveArray ();
				}

				message.EndArray ();

				session.Send (message);
			}
		}
	}
}

