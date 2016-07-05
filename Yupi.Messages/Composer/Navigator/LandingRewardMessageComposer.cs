using System;

using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Navigator
{
	public class LandingRewardMessageComposer : AbstractComposer<HotelLandingManager>
	{
		public override void Compose (Yupi.Protocol.ISender session, HotelLandingManager manager)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(manager.FurniRewardName);
				message.AppendInteger(manager.FurniRewardId);
				message.AppendInteger(120); // TODO Magic number
				message.AppendInteger(120 - session.GetHabbo().Respect);
				session.Send (message);
			}
		}
	}
}

