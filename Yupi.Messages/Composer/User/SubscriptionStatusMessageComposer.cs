using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.User
{
	public class SubscriptionStatusMessageComposer : AbstractComposer<Subscription>
	{
		public override void Compose (Yupi.Protocol.ISender session, Subscription subscription)
		{ // TODO refactor
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString("club_habbo");

				if (subscription != null) {
					int days = (int)Math.Ceiling (subscription.ExpireTime - Yupi.GetUnixTimeStamp () / 86400.0);
					int activeFor =
						(int)
					Math.Ceiling ((Yupi.GetUnixTimeStamp () -
						(double)subscription.ActivateTime) /
						86400.0);
				
					int months = days / 31;

					if (months >= 1)
						months--;

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

