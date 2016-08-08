using System;
using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;
using System.Collections.Generic;
using System.Linq;
using Yupi.Model.Domain.Components;

namespace Yupi.Messages.User
{
	public class LoadBadgesWidgetMessageComposer : Contracts.LoadBadgesWidgetMessageComposer
	{
		public override void Compose (Yupi.Protocol.ISender session, UserBadgeComponent badges)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger (badges.Badges.Count);

				foreach (Badge badge in badges) {
					message.AppendInteger(1); // TODO Magic constant
					message.AppendString(badge.Code);
				}

				IList<Badge> visibleBadges = badges.GetVisible ();

				message.AppendInteger (visibleBadges.Count);

				foreach (Badge badge in visibleBadges) {
					message.AppendInteger(badge.Slot);
					message.AppendString(badge.Code);
				}

				session.Send (message);
			}
		}
	}
}

