using System;

using Yupi.Protocol.Buffers;
using Yupi.Model.Domain;

namespace Yupi.Messages.Navigator
{
	public class LandingRewardMessageComposer : AbstractComposer<HotelLandingManager, Habbo>
	{
		public override void Compose (Yupi.Protocol.ISender session, HotelLandingManager manager, Habbo user)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendString(manager.FurniReward.Name);
				message.AppendInteger(manager.FurniReward.Id);
				message.AppendInteger(120); // TODO Magic constant
				message.AppendInteger(120 - user.Respect);
				session.Send (message);
			}
		}
	}
}

