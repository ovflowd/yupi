using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.User
{
	public class SubscriptionStatusMessageComposer : AbstractComposer<Subscription>
	{
		public override void Compose (Yupi.Protocol.ISender session, Subscription subscription)
		{ // TODO refactor
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString("club_habbo");

				if (subscription != null) {
					int days = (subscription.ExpireTime - DateTime.Now).Days;
					int activeFor = (DateTime.Now - subscription.ActivateTime).Days;
					int months = days / 31;

					message.AppendInteger (days - months * 31);
					message.AppendInteger (1);
					message.AppendInteger (months);
					message.AppendInteger (1);
					message.AppendBool (true);
					message.AppendBool (true);
					message.AppendInteger (activeFor);
					message.AppendInteger (activeFor);
					message.AppendInteger (10);
				} else {
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendBool(false);
					message.AppendBool(false);
					message.AppendInteger(0);
					message.AppendInteger(0);
					message.AppendInteger(0);
				}
				session.Send (message);
			}
		}
	}
}

