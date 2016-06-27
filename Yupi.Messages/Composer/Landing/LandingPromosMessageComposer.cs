using System;
using System.Collections.Generic;
using Yupi.Emulator.Game.Browser.Models;
using Yupi.Protocol.Buffers;

namespace Yupi.Messages.Landing
{
	public class LandingPromosMessageComposer : AbstractComposer<List<HotelLandingPromos>>
	{
		public override void Compose (Yupi.Protocol.ISender session, List<HotelLandingPromos> promos)
		{
			using (ServerMessage message = Pool.GetMessageBuffer (Id)) {
				message.AppendInteger(promos.Count);

				foreach (HotelLandingPromos promo in promos) {
					promo.Serialize (message);
				}
				
				session.Send (message);
			}
		}
	}
}

